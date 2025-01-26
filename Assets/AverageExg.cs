using OpenBCI.Network.Streams;
using UnityEngine;

public class AverageExg : MonoBehaviour
{
    [SerializeField] public EXGStream Stream; 
    public float[] averages;

    void Start()
    {
        if (Stream != null)
        {
            averages = new float[Stream.ChannelCount];
        }
        else
        {
            Debug.LogWarning("Stream is not assigned.");
        }
    }

    void Update()
    {
        if (Stream != null)
        {
            for (int i = 0; i < Stream.ChannelCount; i++)
            {
                var currentStreamChannelData = Stream.GetData(i);

                if (currentStreamChannelData.Length > 0)
                {
                    float sum = 0f;

                    foreach (var value in currentStreamChannelData)
                    {
                        sum += value;
                    }

                    float average = sum / currentStreamChannelData.Length;

                    averages[i] = Mathf.Clamp01((average + 1.5f) / 3.0f);
                }
                else
                {
                    averages[i] = 0;
                }
            }
        }
        else
        {
            Debug.LogError("Stream is still not assigned.");
        }
    }

}
