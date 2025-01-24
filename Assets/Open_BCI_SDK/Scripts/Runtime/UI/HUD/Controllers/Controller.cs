using System;
using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public abstract class Controller : MonoBehaviour
    {
        public float Up;
        public float Down;
        public float Left;
        public float Right;
        public float Forward;
        public float Backward;
        public float YawRight;
        public float YawLeft;

        [Range(0f, 1f), SerializeField] private float VerticalSensitivity = 1f;
        [Range(0f, 1f), SerializeField] private float HorizontalSensitivity = 1f;
        [Range(0f, 1f), SerializeField] private float ForwardSensitivity = 1f;
        [Range(0f, 1f), SerializeField] private float YawSensitivity = 1f;

        public Action TakeoffLand;
        public Action TakePicture;
        public Action Arm;

        public Vector3 GetTranslation()
        {
            return new Vector3(
                (Right - Left) * HorizontalSensitivity,
                (Up - Down) * VerticalSensitivity,
                (Forward - Backward) * ForwardSensitivity);
        }

        public float GetYaw()
        {
            return (YawRight - YawLeft) * YawSensitivity;
        }
    }
}