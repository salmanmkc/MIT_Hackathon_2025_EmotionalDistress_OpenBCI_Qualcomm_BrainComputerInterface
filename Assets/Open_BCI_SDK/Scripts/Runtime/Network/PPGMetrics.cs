using System;
using UnityEngine;

namespace OpenBCI.Network
{
    [Serializable]
    public struct PPGMetrics
    {
        public float HeartRate;
        public float HeartRateVariability;
        public float BloodOxygen;
    }
    
}