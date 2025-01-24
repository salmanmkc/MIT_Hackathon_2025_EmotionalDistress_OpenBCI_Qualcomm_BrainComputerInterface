using UnityEngine;

namespace OpenBCI.Markers
{
    public class ToggleMarker : Marker
    {
        [SerializeField] private double ToggleOnValue = 1;
        [SerializeField] private double ToggleOffValue = 2;

        private void OnEnable()
        {
            AddStreamMarker(ToggleOnValue);
        }

        private void OnDisable()
        {
            AddStreamMarker(ToggleOffValue);
        }
    }
}