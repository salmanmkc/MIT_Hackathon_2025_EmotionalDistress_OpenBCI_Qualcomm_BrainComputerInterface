using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class PPGMetricsGraph : TimeSeriesGraph
    {
        [SerializeField] private PPGMetricsStream Stream;
        
        protected override int GetNumberChannels() => 3;

        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            Assert.AreEqual(3, channels.Length);

            channels[0].UpdateData(Stream.GetHeartRateData());
            channels[1].UpdateData(Stream.GetHeartRateVariabilityData());
            channels[2].UpdateData(Stream.GetBloodOxygenData());
        }
    }
}