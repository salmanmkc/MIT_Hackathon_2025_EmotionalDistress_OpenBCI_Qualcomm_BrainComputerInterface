using OpenBCI.Network.Streams;
using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class EXGGraph : TimeSeriesGraph
    {
        [SerializeField] private EXGStream Stream;
        
        protected override int GetNumberChannels() => Stream ? Stream.ChannelCount : 0;

        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            for (var i = 0; i < Stream.ChannelCount; i++)
            {
                channels[i].UpdateData(Stream.GetData(i));
            }
        }
    }
}