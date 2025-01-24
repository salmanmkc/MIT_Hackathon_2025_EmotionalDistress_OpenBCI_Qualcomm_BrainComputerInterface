using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Lines
{
    public class DefaultGraphLine : TimeSeriesGraphLine
    {
        public override void UpdateData(float[] data)
        {
            if (data == null || data.Length == 0) return;
            
            UpdateHorizontalSpacing(data.Length);

            var min = float.MaxValue;
            var max = float.MinValue;
            foreach (var x in data)
            {
                if (x < min) min = x;
                if (x > max) max = x;
            }

            for (var i = 0; i < data.Length; i++)
            {
                var value = data[i];

                var remapped = 0f;
                if (!Mathf.Approximately(min, max))
                {
                    remapped = (value - min) * MaxHeight / (max - min);
                }

                Positions[i].y = remapped;
            }

            GraphLine.SetPositions(Positions);
        }
    }
}