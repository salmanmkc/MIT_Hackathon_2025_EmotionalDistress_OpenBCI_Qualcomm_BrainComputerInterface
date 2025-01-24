using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public class GaleaAuxStream : TwoDimensionalStream
    {
        [SerializeField] private uint WindowSize;

        private RingBuffer redPPGBuffer;
        private RingBuffer infraredPPGBuffer;
        private RingBuffer edaBuffer;

        public float[] GetRedPPGData() => redPPGBuffer.Data;
        public float[] GetInfraredPPGData() => infraredPPGBuffer.Data;
        public float[] GetEDAData() => edaBuffer.Data;

        private void Awake()
        {
            redPPGBuffer = new RingBuffer(WindowSize);
            infraredPPGBuffer = new RingBuffer(WindowSize);
            edaBuffer = new RingBuffer(WindowSize);
        }

        protected override void ProcessData(float[,] data)
        {
            for (var i = 0; i < data.GetLength(1); i++)
            {
                redPPGBuffer.Insert(data[0, i]);
                infraredPPGBuffer.Insert(data[1, i]);
                edaBuffer.Insert(data[2, i]);
            }
        }
    }
}