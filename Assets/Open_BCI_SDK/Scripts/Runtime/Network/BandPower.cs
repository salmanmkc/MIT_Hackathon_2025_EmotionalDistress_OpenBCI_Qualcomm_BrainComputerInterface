using System;
using UnityEngine;

namespace OpenBCI.Network
{
    [Serializable]
    public struct BandPower
    {
        [Range(0, 1)] public float Alpha;
        [Range(0, 1)] public float Beta;
        [Range(0, 1)] public float Gamma;
        [Range(0, 1)] public float Delta;
        [Range(0, 1)] public float Theta;
    }
}