namespace OpenBCI.Network.Streams
{
    public class AverageBandPowerStream : OneDimensionalStream
    {
        public BandPower AverageBandPower;

        protected override void ProcessData(float[] data)
        {
            AverageBandPower.Delta = data[0];
            AverageBandPower.Theta = data[1];
            AverageBandPower.Alpha = data[2];
            AverageBandPower.Beta = data[3];
            AverageBandPower.Gamma = data[4];
        }
    }
}