using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.Network.Streams
{
    public class EMGStream : OneDimensionalStream
    {
        [Range(4, 24)]
        public int ChannelCount;
        public float[] Channels;

        private void Awake()
        {
            Channels = new float[ChannelCount];
        }

        protected override void ProcessData(float[] data)
        {
            Assert.AreEqual(Channels.Length, data.Length);
            Channels = data;
        }
    }
}