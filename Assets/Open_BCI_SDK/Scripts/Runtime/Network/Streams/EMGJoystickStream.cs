using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class EMGJoystickStream : OneDimensionalStream
    {
        public Vector2 Joystick;

        protected override void ProcessData(float[] data)
        {
            Joystick.x = data[0];
            Joystick.y = data[1];
        }
    }
}