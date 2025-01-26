using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class EXGStream2 : TwoDimensionalStream
    {
        [Range(4, 24)] public int ChannelCount;
        [SerializeField] private uint WindowSize;

        private RingBuffer[] buffers;

        public float[] GetData(int channelIndex) => buffers[channelIndex].Data;

        //public float GetAverageFifthSample(int channelIndex) => buffers[channelIndex].GetSampleAverage();

        private void Awake()
        {
            buffers = new RingBuffer[ChannelCount];
            for (var i = 0; i < ChannelCount; i++)
            {
                buffers[i] = new RingBuffer(WindowSize);
            }
        }

        protected override void ProcessData(float[,] data)
        {
            for (var i = 0; i < data.GetLength(1); i++)
            {
                for (var channel = 0; channel < ChannelCount; channel++)
                {
                    buffers[channel].Insert(data[channel, i]);
                }
            }
        }
    }
}