using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class IMUStream : TwoDimensionalStream
    {
        [SerializeField] private uint WindowSize;

        private Vector3[] accelerometer;
        private Vector3[] gyroscope;
        private Vector3[] magnetometer;
        
        private RingBuffer[] accelerometerBuffer;
        private RingBuffer[] gyroscopeBuffer;
        private RingBuffer[] magnetometerBuffer;

        private void Awake()
        {
            accelerometer = new Vector3[WindowSize];
            gyroscope = new Vector3[WindowSize];
            magnetometer = new Vector3[WindowSize];
            
            accelerometerBuffer = new RingBuffer[3];
            gyroscopeBuffer = new RingBuffer[3];
            magnetometerBuffer = new RingBuffer[3];

            for (var i = 0; i < 3; i++)
            {
                accelerometerBuffer[i] = new RingBuffer(WindowSize);
                gyroscopeBuffer[i] = new RingBuffer(WindowSize);
                magnetometerBuffer[i] = new RingBuffer(WindowSize);
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
        
        public Vector3[] GetGyroscopeData()
        {
            var xData = gyroscopeBuffer[0].Data;
            var yData = gyroscopeBuffer[1].Data;
            var zData = gyroscopeBuffer[2].Data;
            
            for (var i = 0; i < WindowSize; i++)
            {
                gyroscope[i].x = xData[i];
                gyroscope[i].y = yData[i];
                gyroscope[i].z = zData[i];
            }

            return gyroscope;
        }
        
        public Vector3[] GetMagnetometerData()
        {
            var xData = magnetometerBuffer[0].Data;
            var yData = magnetometerBuffer[1].Data;
            var zData = magnetometerBuffer[2].Data;
            
            for (var i = 0; i < WindowSize; i++)
            {
                magnetometer[i].x = xData[i];
                magnetometer[i].y = yData[i];
                magnetometer[i].z = zData[i];
            }

            return magnetometer;
        }

        protected override void ProcessData(float[,] data)
        {
            for (var sample = 0; sample < data.GetLength(1); sample++)
            {
                for (var axis = 0; axis < 3; axis++)
                {
                    accelerometerBuffer[axis].Insert(data[axis, sample]);
                    gyroscopeBuffer[axis].Insert(data[axis + 3, sample]);
                    magnetometerBuffer[axis].Insert(data[axis + 6, sample]);
                }
            }
        }
    }
}