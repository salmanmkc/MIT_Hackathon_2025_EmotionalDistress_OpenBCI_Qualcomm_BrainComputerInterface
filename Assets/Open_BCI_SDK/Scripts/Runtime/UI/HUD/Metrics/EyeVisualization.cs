using OpenBCI.EyeTracking;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace OpenBCI.UI.HUD
{
    public class EyeVisualization : MonoBehaviour
    {
        private enum EyePicker
        {
            Left,
            Right
        }
        [SerializeField] private EyeGazeManager EyeTracker;
        [SerializeField] private GameObject OuterEye;
        [SerializeField] private GameObject Pupil;
        [SerializeField] private EyePicker EyeToUse;

        private float minPupilDiameter = float.MaxValue;
        private float maxPupilDiameter = float.MinValue;

        private Quaternion defaultPupilRotation;

        private void OnValidate()
        {
            EyeTracker = FindObjectOfType<EyeGazeManager>(true);
        }

        private void Awake()
        {
            OnValidate();
        }

        private void Start()
        {
            defaultPupilRotation = Pupil.transform.localRotation;
        }

        private void Update()
        {
            //Get pupil diameter and scale the value based on the min and max values over time
            var pupilDiameter = EyeToUse == EyePicker.Left ? EyeTracker.PupilSize.x : EyeTracker.PupilSize.y;

            if (pupilDiameter < minPupilDiameter)
            {
                minPupilDiameter = pupilDiameter;
            }
            if (pupilDiameter > maxPupilDiameter)
            {
                maxPupilDiameter = pupilDiameter;
            }
            var pupilDiameterScaled = Mathf.InverseLerp(minPupilDiameter, maxPupilDiameter, pupilDiameter);
            Pupil.transform.localScale = new Vector3(pupilDiameterScaled, pupilDiameterScaled, 1);

            //Get gaze direction and rotate the outer eye
            var gazeDirection = EyeTracker.GazeDirection;
            var gazeDirectionX = defaultPupilRotation.x + gazeDirection.x;
            var gazeDirectionY = defaultPupilRotation.z + gazeDirection.y;
            var gazeDirectionZ = defaultPupilRotation.z + gazeDirection.z;
            var gazeDirectionRotated = new Quaternion(defaultPupilRotation.x, defaultPupilRotation.y, gazeDirectionY, 1);
            Pupil.transform.localRotation = gazeDirectionRotated;
        }
    }
}