using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class AccelerometerStream : TwoDimensionalStream
    {
        [SerializeField] private uint WindowSize;
        
        private Vector3[] accelerometer;
        private RingBuffer[] accelerometerBuffer;
        
        private void Awake()
        {
            accelerometer = new Vector3[WindowSize];
            accelerometerBuffer = new RingBuffer[3];

            for (var i = 0; i < 3; i++)
            {
                accelerometerBuffer[i] = new RingBuffer(WindowSize);
            }
        }
        
        public Vector3[] GetAccelerometerData()
        {
            var xData = accelerometerBuffer[0].Data;
            var yData = accelerometerBuffer[1].Data;
            var zData = accelerometerBuffer[2].Data;
            
            for (var i = 0; i < WindowSize; i++)
            {
                accelerometer[i].x = xData[i];
                accelerometer[i].y = yData[i];
                accelerometer[i].z = zData[i];
            }

            return accelerometer;
        }
        
        protected override void ProcessData(float[,] data)
        {
            for (var sample = 0; sample < data.GetLength(1); sample++)
            {
                for (var axis = 0; axis < 3; axis++)
                {
                    accelerometerBuffer[axis].Insert(data[axis, sample]);
                }
            }
        }
    }
}