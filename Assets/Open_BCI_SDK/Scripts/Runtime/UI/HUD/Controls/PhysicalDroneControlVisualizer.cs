using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class PhysicalDroneControlVisualizer : ControlVisualizer
    {
        [Space]
        [SerializeField] private ToggledControl JoystickControl;
        [SerializeField] private ToggledControl FlightStatus;
        [SerializeField] private DroneAttitude Attitude;

        private bool isFlying;

        private void Start()
        {
            Controller.TakeoffLand += () => isFlying = !isFlying;
        }

        protected override void Update()
        {
            base.Update();

            FlightStatus.SetActive(isFlying);

            Attitude.UpdateStrafe(Controller.GetTranslation().x);
        }
    }
}