using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Lines
{
    public class FFTGraphLine : TimeSeriesGraphLine
    {
        public override void UpdateData(float[] data)
        {
            if (data == null || data.Length == 0) return;
            UpdateHorizontalSpacing(data.Length);
            
            for (var i = 0; i < data.Length; i++)
            {
                Positions[i].y = Mathf.Clamp(data[i], 0f, MaxHeight);
            }
            
            GraphLine.SetPositions(Positions);
        }
    }
}