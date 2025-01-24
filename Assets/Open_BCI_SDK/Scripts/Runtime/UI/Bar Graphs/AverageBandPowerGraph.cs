using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI.BarGraphs
{
    public class AverageBandPowerGraph : MonoBehaviour
    {
        [SerializeField] private AverageBandPowerStream Stream;
        [SerializeField] private BandPowerGraph Graph;

        private void Awake()
        {
            Assert.IsNotNull(Stream);
        }

        private void Update()
        {
            Graph.UpdateBandPowers(Stream.AverageBandPower);
        }
    }
}