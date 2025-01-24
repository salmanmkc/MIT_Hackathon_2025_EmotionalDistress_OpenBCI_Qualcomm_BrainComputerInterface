using System.Collections.Generic;
using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class HeartDisplay : MonoBehaviour
    {
        public Renderer HighlightMeshRenderer;
        public float Strength;
        public Color High;
        public Color Low;
        public float HeartRate;

        private Queue<float> heartRateSamples;
        private Material material;
        private float Interval => 1 / (HeartRate / 60);
        private float timer;

        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            material = HighlightMeshRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor(EmissionColor, Color.black);
            heartRateSamples = new Queue<float>();
        }

        private void Update()
        {
            var heartRate = 60;
            if (heartRate is > 20 and < 150)
            {
                heartRateSamples.Enqueue((float)heartRate);
                if (heartRateSamples.Count > 20) heartRateSamples.Dequeue();
                float sum = 0;
                foreach (float sample in heartRateSamples)
                {
                    sum += sample;
                }

                HeartRate = sum / heartRateSamples.Count;
            }

            timer += Time.deltaTime;
            if (timer >= Interval)
            {
                // If the timer is greater than the interval, then we have a beat
                Strength = 1;
                timer = 0;
            }
            else
            {
                // If the timer is less than the interval, then we are in between beats
                Strength *= 0.9f; // Fade out the strength
            }

            material.SetColor(EmissionColor, Color.Lerp(Low, High, Strength));

        }
    }
}