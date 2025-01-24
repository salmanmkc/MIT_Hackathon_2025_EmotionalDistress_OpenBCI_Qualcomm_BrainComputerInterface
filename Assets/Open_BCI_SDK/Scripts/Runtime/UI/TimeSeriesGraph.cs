using System.Linq;
using UnityEngine;

namespace OpenBCI.UI
{
    public abstract class TimeSeriesGraph : MonoBehaviour
    {
        [SerializeField] private TimeSeriesGraphLine[] Channels;

        protected abstract int GetNumberChannels();
        protected abstract void UpdateData(TimeSeriesGraphLine[] channels);
        
        private void Update() => UpdateData(Channels);

        private void OnValidate()
        {
            if (Channels == null || Channels.Length != GetNumberChannels())
            {
                var lines = GetComponentsInChildren<TimeSeriesGraphLine>(true);
                Channels = lines.Take(GetNumberChannels()).ToArray();
            }
        }

        private void Awake()
        {
            OnValidate();
            if (Channels.Length != GetNumberChannels())
            {
                Debug.LogError("Time Series Graph does not have the correct number of graph lines");
                enabled = false;
            }
        }
    }
}