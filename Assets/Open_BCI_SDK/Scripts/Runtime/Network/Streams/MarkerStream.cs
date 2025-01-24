using System;
using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class MarkerStream : OneDimensionalStream
    {
        public Action<float> MarkerReceived;
        
        [SerializeField] private uint WindowSize;
        
        private RingBuffer buffer;
        
        public float[] GetMarkerData() => buffer.Data;

        private void Awake()
        {
            buffer = new RingBuffer(WindowSize);
        }
        
        protected override void ProcessData(float[] data)
        {
            foreach (var sample in data)
            {
                buffer.Insert(sample);
                if (sample != 0f) MarkerReceived?.Invoke(sample);
            }
        }
    }
}