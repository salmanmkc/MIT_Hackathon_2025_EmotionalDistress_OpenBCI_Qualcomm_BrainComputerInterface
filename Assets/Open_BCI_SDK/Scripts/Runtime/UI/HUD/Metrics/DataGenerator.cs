using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OpenBCI.UI.HUD
{
    public class DataGenerator : MonoBehaviour
    {
        [SerializeField] protected int BufferSize = 1250;
        [SerializeField] protected int SampleRate = 250;

        public float A;
        public float B;
        public float C;
        public float F;
        public long X;
        public float previousSample;

        protected readonly Queue<float> data = new Queue<float>();

        public DataGenerator()
        {
            for (var i = 0; i < BufferSize; i++)
            {
                data.Enqueue(0);
            }
        }

        public void Update()
        {
            var previousF = F;
            var newF = Random.Range(F - 1, F + 1);
            F = (previousF + newF) / 2;

            var numSamples = SampleRate;
            //var sampleDuration = 1.0f / SampleRate;
            var coefficient1 = A;
            var coefficient2 = Mathf.Cos((Mathf.PI * X) / F);
            var coefficient3 = (B * Mathf.Cos(X / F));
            var coefficient4 = Mathf.Cos(X / C) * C * Mathf.PI;
            var sampleValue = coefficient1 * coefficient2 * coefficient3 * coefficient4;

            var sampleToQueue = Mathf.Clamp(((sampleValue + previousSample) / 2), -1, 1);
            data.Enqueue(sampleToQueue);
            previousSample = sampleToQueue;

            if (data.Count > BufferSize)
            {
                data.Dequeue();
            }

            X++;
            if (X == long.MaxValue) X = 0;
        }

        public Queue<float> GetSamples()
        {
            return new Queue<float>(data);
        }
    }
}