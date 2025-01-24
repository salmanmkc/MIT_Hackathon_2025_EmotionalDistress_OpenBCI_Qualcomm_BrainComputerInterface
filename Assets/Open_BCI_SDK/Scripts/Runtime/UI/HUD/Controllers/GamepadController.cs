using UnityEngine;
using UnityEngine.InputSystem;

namespace OpenBCI.UI.HUD
{
    public class GamepadController : Controller
    {
        [SerializeField] private PlayerInput PlayerInput;

        private void OnValidate()
        {
            PlayerInput ??= GetComponent<PlayerInput>();
        }

        private void Awake()
        {
            OnValidate();
            if (!PlayerInput.actions) enabled = false;
        }

        private void Start()
        {
            PlayerInput.actions.Enable();
            PlayerInput.onActionTriggered += HandleAction;
        }

        private void HandleAction(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                if (context.action.name == "Move Joystick X") Left = Right = 0f;
                if (context.action.name == "Move Joystick Y") Forward = Backward = 0f;
                if (context.action.name == "Look Joystick X") YawLeft = YawRight = 0f;
                if (context.action.name == "Look Joystick Y") Up = Down = 0f;
            }

            if (!context.performed) return;

            if (context.action.name == "Arm")
            {
                Arm?.Invoke();
            }
            else if (context.action.name == "Takeoff/Land")
            {
                TakeoffLand?.Invoke();
            }
            else if (context.action.name == "Flip")
            {
                TakePicture?.Invoke();
            }
            else if (context.action.name == "Move Joystick X")
            {
                Left = Right = 0f;
                var value = context.ReadValue<float>();

                if (value > 0f) Right = value;
                else Left = -value;
            }
            else if (context.action.name == "Move Joystick Y")
            {
                Forward = Backward = 0f;
                var value = context.ReadValue<float>();

                if (value > 0f) Forward = value;
                else Backward = -value;
            }
            else if (context.action.name == "Look Joystick X")
            {
                YawLeft = YawRight = 0f;
                var value = context.ReadValue<float>();

                if (value > 0f) YawRight = value;
                else YawLeft = -value;
            }
            else if (context.action.name == "Look Joystick Y")
            {
                Up = Down = 0f;
                var value = context.ReadValue<float>();

                if (value > 0f) Up = value;
                else Down = -value;
            }
        }
    }
}