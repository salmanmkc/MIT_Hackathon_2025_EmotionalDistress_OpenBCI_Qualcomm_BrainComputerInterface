namespace OpenBCI.UI.HUD
{
    public class GaleaNetworkController : NetworkController
    {
        protected override void Update()
        {
            // This has been modified to work with Galea. It swaps the physical and EMG joystick inputs.
            Backward = Input.Joystick.y < 0f ? -Input.Joystick.y : 0f;
            Forward = Input.Joystick.y > 0f ? Input.Joystick.y : 0f;
            YawRight = Input.Joystick.x > 0f ? Input.Joystick.x : 0f;
            YawLeft = Input.Joystick.x < 0f ? -Input.Joystick.x : 0f;
        }
    }
}