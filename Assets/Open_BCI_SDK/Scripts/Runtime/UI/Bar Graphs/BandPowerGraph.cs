using OpenBCI.Network;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.BarGraphs
{
    public class BandPowerGraph : BarGraph
    {
        [SerializeField] private BarGraphBar[] Bars;

        private void OnValidate()
        {
            if (Bars == null || Bars.Length == 0) Bars = GetComponentsInChildren<BarGraphBar>();
        }

        private void Awake()
        {
            OnValidate();
            Assert.AreEqual(5, Bars.Length);
        }

        public void UpdateBandPowers(BandPower powers)
        {
            UpdateBar(Bars[0], powers.Delta * 100f);
            UpdateBar(Bars[1], powers.Theta * 100f);
            UpdateBar(Bars[2], powers.Alpha * 100f);
            UpdateBar(Bars[3], powers.Beta * 100f);
            UpdateBar(Bars[4], powers.Gamma * 100f);
        }
    }
}