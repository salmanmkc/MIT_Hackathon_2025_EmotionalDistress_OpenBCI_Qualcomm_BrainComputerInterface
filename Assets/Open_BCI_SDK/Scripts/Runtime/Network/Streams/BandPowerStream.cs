using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.Network.Streams
{
    public class BandPowerStream : TwoDimensionalStream
    {
        [Range(4, 24)] public int ChannelCount;
        public BandPower[] Channels;

        private void Awake()
        {
            Channels = new BandPower[ChannelCount];
        }

        protected override void ProcessData(float[,] data)
        {
            Assert.AreEqual(Channels.Length, data.GetLength(0));
            Assert.AreEqual(5, data.GetLength(1));

            for (var i = 0; i < Channels.Length && i < data.GetLength(0); i++)
            {
                Channels[i].Delta = data[i, 0];
                Channels[i].Theta = data[i, 1];
                Channels[i].Alpha = data[i, 2];
                Channels[i].Beta = data[i, 3];
                Channels[i].Gamma = data[i, 4];
            }
        }
    }
}