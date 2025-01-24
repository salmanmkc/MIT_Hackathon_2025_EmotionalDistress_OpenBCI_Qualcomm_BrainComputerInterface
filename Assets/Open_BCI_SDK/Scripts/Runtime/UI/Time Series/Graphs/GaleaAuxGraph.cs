using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class GaleaAuxGraph : TimeSeriesGraph
    {
        [SerializeField] private GaleaAuxStream Stream;
        
        protected override int GetNumberChannels() => 3;

        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            Assert.AreEqual(3, channels.Length);
            
            channels[0].UpdateData(Stream.GetRedPPGData());
            channels[1].UpdateData(Stream.GetInfraredPPGData());
            channels[2].UpdateData(Stream.GetEDAData());
        }
    }
}