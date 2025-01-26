import argparse
import sys
import time
import socket
import numpy as np
import cv2
import json
import random
from picamera2 import CompletedRequest, MappedArray, Picamera2
from picamera2.devices.imx500 import IMX500, NetworkIntrinsics
from picamera2.devices.imx500.postprocess import COCODrawer
from picamera2.devices.imx500.postprocess_highernet import postprocess_higherhrnet
from datetime import datetime


# UDP setup
UDP_IP = "10.29.205.94"  # Unity's IP address
UDP_PORT = 9876      # Unity's UDP port
udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Haar Cascade models for face and smile detection
face_cascade = cv2.CascadeClassifier("./haarcascades/haarcascade_frontalface_default.xml")
smile_cascade = cv2.CascadeClassifier("./haarcascades/haarcascade_smile.xml")

# Global Variables
last_boxes = None
last_scores = None
last_keypoints = None
WINDOW_SIZE_H_W = (480, 640)

def ai_output_tensor_parse(metadata: dict):
    """Parse the output tensor into detected objects scaled to the ISP output."""
    global last_boxes, last_scores, last_keypoints
    np_outputs = imx500.get_outputs(metadata=metadata, add_batch=True)
    if np_outputs is not None:
        keypoints, scores, boxes = postprocess_higherhrnet(
            outputs=np_outputs,
            img_size=WINDOW_SIZE_H_W,
            img_w_pad=(0, 0),
            img_h_pad=(0, 0),
            detection_threshold=args.detection_threshold,
            network_postprocess=True
        )

        if scores is not None and len(scores) > 0:
            last_keypoints = np.reshape(np.stack(keypoints, axis=0), (len(scores), 17, 3))
            last_boxes = [np.array(b) for b in boxes]
            last_scores = np.array(scores)
    return last_boxes, last_scores, last_keypoints


def ai_output_tensor_draw(request: CompletedRequest, boxes, scores, keypoints, stream='main'):
    """Draw the detections for this request onto the ISP output."""
    with MappedArray(request, stream) as m:
        if boxes is not None and len(boxes) > 0:
            drawer.annotate_image(m.array, boxes, scores,
                                  np.zeros(scores.shape), keypoints, args.detection_threshold,
                                  args.detection_threshold, request.get_metadata(), picam2, stream)

        # Perform smile detection
        smiling = detect_smile(m.array)

        # Analyze tension and send data
        tension = calculate_tension(smiling, keypoints)
        send_tension_data(tension)


def detect_smile(image):
    """
    Detect smiles in the image using Haar cascades.
    """
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)  # Convert to grayscale
    if gray is None or gray.size == 0:
        print("Error: Input image is empty or invalid!")
        return 0

    # Check if face cascade is loaded
    if face_cascade.empty():
        print("Error: Face cascade not loaded!")
        return 0

    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))
    if len(faces) == 0:
        print("No faces detected.")
        return 0

    for (x, y, w, h) in faces:
        roi_gray = gray[y:y + h, x:x + w]  # Crop face region
        smiles = smile_cascade.detectMultiScale(roi_gray, scaleFactor=1.7, minNeighbors=22, minSize=(25, 25))
        if len(smiles) > 0:
            cv2.rectangle(image, (x, y), (x + w, y + h), (255, 0, 0), 2)  # Annotate face
            #cv2.putText(image, "Smiling!", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 0), 2)
            return 1  # Smiling detected

    return 0  # No smile detected



def calculate_tension(smiling, keypoints):
    """
    Calculate tension value based on smiling and body keypoints.
    """
    body_tension = analyze_body_tension(keypoints)
    # Tension is higher when there is no smile and body appears tense.
    tension = (1 - smiling) * 0.6 + body_tension * 0.4
    return round(min(max(tension, 0.0), 1.0), 2)  # Normalize and round tension value


## Debugging UDP communication
"""
def calculate_tension(smiling, keypoints):
    # If smiling or no movement, tension is low
    if smiling == 0:
        # Return a random tension value between 0.0 and 1.0
        return round(random.uniform(0.0, 1.0), 2)

    # If smiling or no movement, tension is low
    return round(random.uniform(0.0, 1.0), 2)  # Return random tension value between 0.0 and 1.0
"""


def analyze_body_tension(keypoints):
    """
    Analyze body tension based on keypoints.
    """
    if keypoints is None or len(keypoints) == 0:
        return 0.0  # No person detected, assume relaxed.

    for person_keypoints in keypoints:
        # Extract keypoints (example: OpenPose format keypoints)
        nose = person_keypoints[0]
        left_shoulder = person_keypoints[5]
        right_shoulder = person_keypoints[6]
        left_hand = person_keypoints[9]
        right_hand = person_keypoints[10]

        # Example heuristic: hands close to face or shoulders hunched
        if left_hand[2] > 0.5 and nose[2] > 0.5:  # Visibility check
            hand_to_face_distance = np.linalg.norm(np.array(left_hand[:2]) - np.array(nose[:2]))
            if hand_to_face_distance < 50:  # Arbitrary threshold
                return 1.0  # High tension

        if right_hand[2] > 0.5 and nose[2] > 0.5:  # Visibility check
            hand_to_face_distance = np.linalg.norm(np.array(right_hand[:2]) - np.array(nose[:2]))
            if hand_to_face_distance < 50:  # Arbitrary threshold
                return 1.0  # High tension

    return 0.0  # Low tension


def send_tension_data(tension):
    """Send tension value via UDP."""
    udp_data = {
        "tension": tension,
    }
    udp_socket.sendto(json.dumps(udp_data).encode(), (UDP_IP, UDP_PORT))


def picamera2_pre_callback(request: CompletedRequest):
    """Analyse the detected objects in the output tensor and draw them on the main output image."""
    boxes, scores, keypoints = ai_output_tensor_parse(request.get_metadata())
    ai_output_tensor_draw(request, boxes, scores, keypoints)


def get_args():
    parser = argparse.ArgumentParser()
    parser.add_argument("--model", type=str, help="Path of the model",
                        default="/usr/share/imx500-models/imx500_network_higherhrnet_coco.rpk")
    parser.add_argument("--fps", type=int, help="Frames per second")
    parser.add_argument("--detection-threshold", type=float, default=0.3,
                        help="Post-process detection threshold")
    parser.add_argument("--labels", type=str,
                        help="Path to the labels file")
    parser.add_argument("--print-intrinsics", action="store_true",
                        help="Print JSON network_intrinsics then exit")
    return parser.parse_args()


def get_drawer():
    categories = intrinsics.labels
    categories = [c for c in categories if c and c != "-"]
    return COCODrawer(categories, imx500, needs_rescale_coords=False)


if __name__ == "__main__":
    args = get_args()

    # Initialize IMX500 and Picamera2
    imx500 = IMX500(args.model)
    intrinsics = imx500.network_intrinsics or NetworkIntrinsics()
    intrinsics.update_with_defaults()

    if args.print_intrinsics:
        print(intrinsics)
        exit()

    drawer = get_drawer()
    picam2 = Picamera2(imx500.camera_num)
    config = picam2.create_preview_configuration(controls={'FrameRate': intrinsics.inference_rate}, buffer_count=12)

    picam2.start(config, show_preview=True)
    imx500.set_auto_aspect_ratio()
    picam2.pre_callback = picamera2_pre_callback

    while True:
        time.sleep(20)
