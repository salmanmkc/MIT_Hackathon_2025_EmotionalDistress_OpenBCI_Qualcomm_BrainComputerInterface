using System.Linq;
using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class IMUGraph : TimeSeriesGraph
    {
        [SerializeField] private IMUStream Stream;

        protected override int GetNumberChannels() => 9;
        
        private readonly Vector3GraphData accelerometer = new();
        private readonly Vector3GraphData gyroscope = new();
        private readonly Vector3GraphData magnetometer = new();

        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            Assert.AreEqual(9, channels.Length);
            
            accelerometer.Update(channels.Take(3).ToArray(), Stream.GetAccelerometerData());
            gyroscope.Update(channels.Skip(3).Take(3).ToArray(), Stream.GetGyroscopeData());
            magnetometer.Update(channels.Skip(6).Take(3).ToArray(), Stream.GetMagnetometerData());
        }
    }
}