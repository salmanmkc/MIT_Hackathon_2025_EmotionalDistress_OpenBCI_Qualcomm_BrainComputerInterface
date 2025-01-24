using System;

namespace OpenBCI.Network.Streams
{
    public class GesturePadStream : TwoDimensionalStream
    {
        public GesturePad GesturePad;

        protected override void ProcessData(float[,] data)
        {
            GesturePad.UpTrigger = Convert.ToBoolean(data[0, 0]);
            GesturePad.DownTrigger = Convert.ToBoolean(data[1, 0]);
            GesturePad.LeftTrigger = Convert.ToBoolean(data[2, 0]);
            GesturePad.RightTrigger = Convert.ToBoolean(data[3, 0]);
            GesturePad.UpLeftTrigger = Convert.ToBoolean(data[4, 0]);
            GesturePad.UpRightTrigger = Convert.ToBoolean(data[5, 0]);
            GesturePad.DownLeftTrigger = Convert.ToBoolean(data[6, 0]);
            GesturePad.DownRightTrigger = Convert.ToBoolean(data[7, 0]);
            GesturePad.CenterTrigger = Convert.ToBoolean(data[8, 0]);
            
            GesturePad.Up = data[0, 1];
            GesturePad.Down = data[1, 1];
            GesturePad.Left = data[2, 1];
            GesturePad.Right = data[3, 1];
            GesturePad.UpLeft = data[4, 1];
            GesturePad.UpRight = data[5, 1];
            GesturePad.DownLeft = data[6, 1];
            GesturePad.DownRight = data[7, 1];
            GesturePad.Center = data[8, 1];
        } 
    }
}