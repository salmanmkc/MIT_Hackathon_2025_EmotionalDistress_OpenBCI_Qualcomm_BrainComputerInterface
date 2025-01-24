using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.BarGraphs
{
    public class EMGGraph : BarGraph
    {
        [SerializeField] private EMGStream Stream;
        [SerializeField] private BarGraphBar[] Bars;

        private void OnValidate()
        {
            if (Bars == null || Bars.Length == 0) Bars = GetComponentsInChildren<BarGraphBar>();
        }

        private void Awake()
        {
            OnValidate();
            
            Assert.IsNotNull(Stream);
            Assert.AreEqual(Stream.ChannelCount, Bars.Length);
        }

        private void Update()
        {
            for (var i = 0; i < Stream.Channels.Length && i < Bars.Length; i++)
            {
                UpdateBar(Bars[i], Stream.Channels[i] * 100f);
            }
        }
    }
}