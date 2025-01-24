namespace OpenBCI.Network.Streams
{
    public class FocusStream : SingleValueStream
    {
        public float Focus;
        
        protected override void ProcessData(float data)
        {
            Focus = data;
        }
    }
}