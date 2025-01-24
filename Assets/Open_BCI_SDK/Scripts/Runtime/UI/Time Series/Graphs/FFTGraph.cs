using System.Linq;
using OpenBCI.Network.Streams;
using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class FFTGraph : TimeSeriesGraph
    {
        [Range(4, 24), SerializeField] private int ChannelCount;
        
        [Space]
        [SerializeField] private float Scale = 1f;
        [Range(0, 125), SerializeField] private int MinimumFrequency = 0;
        [Range(0, 125), SerializeField] private int MaximumFrequency = 125;
        
        [SerializeField] private FFTStream Stream;

        protected override int GetNumberChannels() => ChannelCount;

        protected override void UpdateData(TimeSeriesGraphLine[] channels)
        {
            if (Stream == null || Stream.Data == null) return;
            if (MinimumFrequency >= MaximumFrequency) return;
            
            for (var i = 0; i < channels.Length && i < Stream.Data.GetLength(0); i++)
            {
                var data = Enumerable.Range(MinimumFrequency, MaximumFrequency)
                    .Select(x => Mathf.Log10(Stream.Data[i, x]) * Scale).ToArray();
                channels[i].UpdateData(data);
            }
        }
    }
}