using UnityEngine;

namespace OpenBCI.UI
{
    public abstract class BarGraph : MonoBehaviour
    {
        [SerializeField] private float ScaleFactor = 1f;
        [SerializeField] private float MaxHeight = 125f;
        
        protected void UpdateBar(BarGraphBar bar, float value)
        {
            bar.UpdateBar(Mathf.Clamp(value * ScaleFactor, 0f, MaxHeight), value);
        }
    }
}