using System;
using UnityEngine;

namespace OpenBCI.Network
{
    [Serializable]
    public struct GesturePad
    {
        [Range(0, 1)] public float Up;
        [Range(0, 1)] public float Down;
        [Range(0, 1)] public float Left;
        [Range(0, 1)] public float Right;
        [Range(0, 1)] public float UpLeft;
        [Range(0, 1)] public float UpRight;
        [Range(0, 1)] public float DownLeft;
        [Range(0, 1)] public float DownRight;
        [Range(0, 1)] public float Center;
        
        public bool UpTrigger;
        public bool DownTrigger;
        public bool LeftTrigger;
        public bool RightTrigger;
        public bool UpLeftTrigger;
        public bool UpRightTrigger;
        public bool DownLeftTrigger;
        public bool DownRightTrigger;
        public bool CenterTrigger;
    }
}