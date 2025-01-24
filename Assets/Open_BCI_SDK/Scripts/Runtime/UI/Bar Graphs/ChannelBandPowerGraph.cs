using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.BarGraphs
{
    public class ChannelBandPowerGraph : BarGraph
    {
        [SerializeField] private BandPowerStream Stream;
        [SerializeField] private BandPowerGraph[] Channels;

        private void OnValidate()
        {
            if (Channels == null || Channels.Length == 0) Channels = GetComponentsInChildren<BandPowerGraph>();
        }

        private void Awake()
        {
            OnValidate();

            Assert.IsNotNull(Stream);
            Assert.AreEqual(Stream.ChannelCount, Channels.Length);
        }

        private void Update()
        {
            for (var i = 0; i < Channels.Length && i < Stream.Channels.Length; i++)
            {
                Channels[i].UpdateBandPowers(Stream.Channels[i]);
            }
        }
    }
}