using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class AccelerometerGraph : TimeSeriesGraph
    {
        [SerializeField] private AccelerometerStream Stream;
        
        protected override int GetNumberChannels() => 3;

        private readonly Vector3GraphData accelerometer = new();
        
        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            Assert.AreEqual(3, channels.Length);
            accelerometer.Update(channels, Stream.GetAccelerometerData());
        }
    }
}