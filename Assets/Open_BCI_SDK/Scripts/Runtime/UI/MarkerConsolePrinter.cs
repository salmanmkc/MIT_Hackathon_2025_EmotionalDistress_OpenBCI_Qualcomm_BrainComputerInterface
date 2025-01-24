using OpenBCI.Network.Streams;
using UnityEngine;

namespace OpenBCI.UI.TimeSeries.Graphs
{
    public class MarkerConsolePrinter : MonoBehaviour
    {
        [SerializeField] private MarkerStream Stream;

        private void Awake()
        {
            Stream.MarkerReceived += value => Debug.Log("Marker received: " + value);
        }
    }
}