using OpenBCI.Network.Streams;
using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class NetworkController : Controller
    {
        [Space]
        [SerializeField] protected EMGJoystickStream Input;

        private void OnValidate()
        {
            Input ??= FindObjectOfType<EMGJoystickStream>(true);
        }

        private void Awake()
        {
            OnValidate();
        }

        protected virtual void Update()
        {
            Up = Input.Joystick.y < 0f ? -Input.Joystick.y : 0f;
            Down = Input.Joystick.y > 0f ? Input.Joystick.y : 0f;
            Right = Input.Joystick.x > 0f ? Input.Joystick.x : 0f;
            Left = Input.Joystick.x < 0f ? -Input.Joystick.x : 0f;
        }
    }
}