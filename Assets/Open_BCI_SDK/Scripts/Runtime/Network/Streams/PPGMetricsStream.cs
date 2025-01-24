using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class PPGMetricsStream : OneDimensionalStream
    {
        [SerializeField] private uint WindowSize;
        
        private RingBuffer heartRateBuffer;
        private RingBuffer heartRateVariabilityBuffer;
        private RingBuffer bloodOxygenBuffer;
        
        public float[] GetHeartRateData() => heartRateBuffer.Data;
        public float[] GetHeartRateVariabilityData() => heartRateVariabilityBuffer.Data;
        public float[] GetBloodOxygenData() => bloodOxygenBuffer.Data;
        
        private void Awake()
        {
            heartRateBuffer = new RingBuffer(WindowSize);
            heartRateVariabilityBuffer = new RingBuffer(WindowSize);
            bloodOxygenBuffer = new RingBuffer(WindowSize);
        }

        protected override void ProcessData(float[] data)
        {
            heartRateBuffer.Insert(data[0]);
            heartRateVariabilityBuffer.Insert(data[1]);
            bloodOxygenBuffer.Insert(data[2]);
        }
    }
}