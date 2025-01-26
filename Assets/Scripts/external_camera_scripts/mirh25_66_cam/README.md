# Tension Detection and Smile Recognition using AI Camera

## Overview
This script detects the tension level of a person based on their body posture (using body keypoints) and their facial expression (detecting smiles). The calculated tension value (ranging from 0.0 to 1.0) is sent via UDP to another application (e.g., Unity) for further processing. The system utilizes an AI camera (IMX500) and a Raspberry Pi 4 for real-time analysis.

## Features
- Detects smiles using Haar cascades.
- Analyzes body posture to determine tension.
- Sends tension value (between 0.0 and 1.0) via UDP.
- Integration with the Picamera2 library for capturing video frames.
- AI-powered body keypoint detection for improved accuracy.

## Hardware Requirements
- Raspberry Pi 4:
- Minimum 2GB of RAM (recommended 4GB).
- Raspbian OS installed.
- Python 3.x environment.
- AI Camera (IMX500): IMX500 camera module to capture video frames and analyze human poses. The camera uses the Picamera2 library to interface with the Raspberry Pi.

## Software Dependencies
To run the script, you'll need the following Python packages and tools installed:
Required Python Libraries:
numpy: For numerical operations and array handling.
opencv-python: For image processing and facial detection.
picamera2: To interface with the IMX500 camera.
json: To send JSON data via UDP.
socket: To send and receive data via UDP.
argparse: To parse command-line arguments.
To Install the Dependencies:
Make sure to use Python 3.x. You can install the required packages using pip:

bash
'''pip install numpy opencv-python picamera2'''
You may need to install the picamera2 library and the necessary camera drivers. Follow the official Picamera2 installation guide to set up your camera.

## Hardware Setup:
Connect the IMX500 AI Camera to the Raspberry Pi 4 using the appropriate interface (MIPI-CSI or USB, depending on your setup).
Ensure the Raspberry Pi is powered on and connected to a network.
Usage
- 1. Run the Python Script:
The script can be executed on the Raspberry Pi directly. To start the process, run the script with the necessary arguments:

bash
'''python tension_smile_detection.py '''

 2. UDP Data Sending:
The script sends the calculated tension value via UDP to a specified IP address and port. You can use a Unity application or any other program to receive and visualize the data.

- UDP Configuration:

 UDP_IP: The IP address of the receiving application (e.g., Unity).
 UDP_PORT: The port for receiving data.
 Example Data Sent:
 The data is sent in JSON format:
  json
  {
    "tension": 0.75
  }

## Real-time Image and Data Processing:
The script captures images, performs pose estimation, detects faces, and calculates the tension value in real-time. The detected data is sent continuously while the camera is capturing frames.


Example Workflow:
Start the Python script on your Raspberry Pi.
Capture video frames and analyze the body posture and facial expressions (detecting smile).
Calculate the tension value based on the pose and smile.
Send tension data to Unity (or any other UDP-enabled receiver).
Receive and process the data on the Unity side.

### Troubleshooting
No faces or smiles detected: Ensure the Haar cascade models (haarcascade_frontalface_default.xml and haarcascade_smile.xml) are correctly placed in the specified directory.
Camera not working: Ensure the Picamera2 library is properly installed and that the camera is connected and configured correctly.
UDP connection issues: Double-check the IP address and port to ensure they match the receiver’s settings.
