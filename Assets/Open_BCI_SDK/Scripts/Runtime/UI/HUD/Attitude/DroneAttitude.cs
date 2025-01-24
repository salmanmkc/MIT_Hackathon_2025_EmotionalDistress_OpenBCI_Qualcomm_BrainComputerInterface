using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class DroneAttitude : MonoBehaviour
    {
        [SerializeField] private float MaxStrafeAngle = 15f;
        [SerializeField] private GameObject DroneWireframe;

        public void UpdateStrafe(float strafe)
        {
            var rotation = DroneWireframe.transform.localRotation.eulerAngles;
            rotation.z = -MaxStrafeAngle * strafe;
            DroneWireframe.transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}