using System;
using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class Vector3GraphData
    {
        private float[] x = Array.Empty<float>();
        private float[] y = Array.Empty<float>();
        private float[] z = Array.Empty<float>();

        public void Update(TimeSeriesGraphLine[] channels, Vector3[] data)
        {
            if (x.Length != data.Length) x = new float[data.Length];
            if (y.Length != data.Length) y = new float[data.Length];
            if (z.Length != data.Length) z = new float[data.Length];
            
            for (var i = 0; i < data.Length; i++)
            {
                x[i] = data[i].x;
                y[i] = data[i].y;
                z[i] = data[i].z;
            }
            
            channels[0].UpdateData(x);
            channels[1].UpdateData(y);
            channels[2].UpdateData(z);
        }
    }
}