using UnityEngine;

namespace OpenBCI.EyeTracking
{
    public class EyeGazeColorChanger : MonoBehaviour, IGazeTarget
    {
        [SerializeField] private Color GazeColor;
        
        private Color originalColor;
        private Material material;
        
        private void Awake()
        {
            material = GetComponent<Renderer>().material;
            originalColor = material.color;
        }

        public void StartGaze(EyeGazeManager source)
        {
            material.color = GazeColor;
        }

        public void StopGaze(EyeGazeManager source)
        {
            material.color = originalColor;
        }
    }
}