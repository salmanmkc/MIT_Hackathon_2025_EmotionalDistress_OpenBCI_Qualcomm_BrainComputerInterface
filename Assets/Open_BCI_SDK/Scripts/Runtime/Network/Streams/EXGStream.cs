using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class EXGStream : TwoDimensionalStream
    {
        [Range(4, 24)] public int ChannelCount;
        [SerializeField] private uint WindowSize;

        private RingBuffer[] buffers;
        
        public float[] GetData(int channelIndex) => buffers[channelIndex].Data;

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