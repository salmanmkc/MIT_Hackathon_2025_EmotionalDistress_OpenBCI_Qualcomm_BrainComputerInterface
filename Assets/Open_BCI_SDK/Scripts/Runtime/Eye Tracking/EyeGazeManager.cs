using System;
using OpenBCI.Varjo;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace OpenBCI.EyeTracking
{
    public class EyeGazeManager : MonoBehaviour
    {
        public Action<EyeGazeManager, IGazeTarget> GazeStarted;
        public Action<EyeGazeManager, IGazeTarget> GazeStopped;

        [Range(0, 1)]
        [SerializeField] private float GazeSmoothing = 0.1f;
        [SerializeField] private float GazeDistance = 0.1f;

        [Space]
        public GameObject FixationPoint;
        
        [Space]
        public Vector2 PupilSize;
        public Vector2 IrisSize;
        public Vector2 Openness;
        public Vector3 GazeDirection;

        public GameObject GazeTarget { get; private set; }

        private Camera mainCamera;
        private Vector3 fixationPoint;
        private EyeTracker tracker;
        
        // Optionally overload this method and use a custom layer mask to increase performance
        // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        protected virtual bool RaycastGazeTargets(Ray ray, out RaycastHit hit) => Physics.Raycast(ray, out hit);

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Start()
        {
            GazeStarted += (manager, target) => target.StartGaze(manager);
            GazeStopped += (manager, target) => target.StopGaze(manager);

            tracker = new EyeTracker();
            tracker.Start();
        }

        private void Update()
        {
            PrintTrackerLog();
            tracker.Update();
            
            if (tracker.Frames.Count == 0) return;
            var frame = tracker.Frames.GetFrame(tracker.Frames.Count - 1);  // Latest frame

            UpdateMetrics(frame);
            UpdateGazeTarget(frame);
        }

        private void UpdateMetrics(Frame frame)
        {
            PupilSize = new Vector2(frame.Pupils.Left, frame.Pupils.Right);
            IrisSize = new Vector2(frame.Irises.Left, frame.Irises.Right);
            Openness = new Vector2(frame.Openness.Left, frame.Openness.Right);
            GazeDirection = new Vector3((float)frame.GazeDirection.X, (float)frame.GazeDirection.Y, (float)frame.GazeDirection.Z);
        }
        
        private void UpdateGazeTarget(Frame frame)
        {
            var currentGazeTarget = GetCurrentGazeTarget(frame);
            if (currentGazeTarget == GazeTarget) return;

            if (GazeTarget) 
            {
                foreach (var target in GazeTarget.GetComponents<IGazeTarget>())
                {
                    GazeStopped.Invoke(this, target);
                }
            }

            if (currentGazeTarget) 
            {
                foreach (var target in currentGazeTarget.GetComponents<IGazeTarget>())
                {
                    GazeStarted.Invoke(this, target);
                }
            }

            GazeTarget = currentGazeTarget;
        }

        private GameObject GetCurrentGazeTarget(Frame frame)
        {
            var cameraTransform = mainCamera.transform;
            var cameraPosition = cameraTransform.position;
            Ray ray;
            
            if (tracker.Status == Status.Calibrated)
            {
                var gaze = new Vector3((float)frame.GazeDirection.X, (float)frame.GazeDirection.Y, (float)frame.GazeDirection.Z);
                var localPosition = gaze * GazeDistance;
                var worldPosition = cameraPosition + cameraTransform.rotation * localPosition;
                fixationPoint = Vector3.Lerp(fixationPoint, worldPosition, GazeSmoothing);
                var direction = fixationPoint - cameraPosition;

                if (FixationPoint) FixationPoint.transform.position = worldPosition;
                ray = new Ray(cameraPosition, direction);
                FixationPoint.SetActive(true);
            }
            else
            {
                ray = new Ray(cameraPosition, cameraTransform.forward);
                FixationPoint.SetActive(false);
            }

            if (!RaycastGazeTargets(ray, out var hit)) return null;
            return hit.transform.gameObject.GetComponent<IGazeTarget>() == null ? null : hit.transform.gameObject;
        }

        private void PrintTrackerLog()
        {
            while (tracker.Log.IsMessageAvailable())
            {
                var entry = tracker.Log.GetMessage();
                Debug.Log($"{entry.Timestamp} | {entry.Type} | {entry.Message}");
            }
        }
    }
}