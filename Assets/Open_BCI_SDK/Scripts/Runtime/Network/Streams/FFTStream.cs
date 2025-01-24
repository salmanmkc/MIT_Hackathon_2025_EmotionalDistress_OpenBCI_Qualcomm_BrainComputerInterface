namespace OpenBCI.Network.Streams
{
    public class FFTStream : TwoDimensionalStream
    {
        public float[,] Data;

        protected override void ProcessData(float[,] data)
        {
            Data = data;
        }
    }
}