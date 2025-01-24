using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.BarGraphs
{
    public class FocusGraph : BarGraph
    {
        [SerializeField] private FocusStream Stream;
        [SerializeField] private BarGraphBar Bar;

        private void OnValidate()
        {
            if (Bar == null) Bar = GetComponentInChildren<BarGraphBar>();
        }

        private void Awake()
        {
            OnValidate();
            
            Assert.IsNotNull(Stream);
            Assert.IsNotNull(Bar);
        }

        private void Update()
        {
            UpdateBar(Bar, Stream.Focus * 100f);
        }
    }
}