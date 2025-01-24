using UnityEngine;
using System;

namespace OpenBCI.UI.HUD
{
    public class DerivativeMetricsMultiChannelGraph : MonoBehaviour
    {
        [SerializeField] protected int WindowSize = 1250;

        [SerializeField] private float MaxHeight = 100f;

        [Space]
        [SerializeField] protected GameObject[] ChannelObjects;
        [Space]
        [SerializeField] private DataGenerator[] DataGenerators;

        private LineRenderer[] graphLines;
        private RectTransform[] transform2D;
        private Vector3[][] positions;

        private void Awake() => OnValidate();

        private void OnValidate()
        {
            graphLines = new LineRenderer[DataGenerators.Length];
            transform2D = new RectTransform[DataGenerators.Length];

            for (var i = 0; i < DataGenerators.Length; i++)
            {
                graphLines[i] = ChannelObjects[i].GetComponentInChildren<LineRenderer>();
                transform2D[i] = ChannelObjects[i].GetComponent<RectTransform>();
            }

            positions = new Vector3[DataGenerators.Length][];
        }

        private void Update()
        {

            var channelIndex = 0;

            foreach (var metric in DataGenerators)
            {

                UpdateHorizontalSpacing(graphLines[channelIndex], channelIndex, WindowSize);

                var data = metric.GetSamples();

                const float min = -1f;
                const float max = 1f;

                var sampleIndex = 0;
                foreach (var value in data)
                {
                    var remapped = 0f;
                    if (!Mathf.Approximately(min, max))
                    {
                        remapped = (value - min) * MaxHeight / (max - min);
                    }

                    try
                    {
                        positions[channelIndex][sampleIndex].y = remapped;
                        sampleIndex++;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(sampleIndex);
                        Debug.Log(e);
                    }

                }

                graphLines[channelIndex].SetPositions(positions[channelIndex]);

                channelIndex++;
            }
        }

        private void UpdateHorizontalSpacing(LineRenderer graphLine, int channelIndex, int windowSize)
        {
            var spacing = transform2D[channelIndex].rect.width / windowSize;
            graphLine.positionCount = windowSize;

            positions[channelIndex] = new Vector3[graphLine.positionCount];
            for (var i = 0; i < windowSize; i++) positions[channelIndex][i].x = spacing * i;
        }
    }
}