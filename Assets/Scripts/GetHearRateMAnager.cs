using UnityEngine;
using UnityEngine.UI;
using OpenBCI.Network.Streams;

public class GetHearRateMAnager : MonoBehaviour
{
[Header("References")]
    public PPGMetricsStream ppgMetricsStream; // Reference to the PPGMetricsStream
    public Slider heartRateSlider; // Reference to the UI slider

    [Header("Heart Rate Range")]
    public float minHeartRate = 40f; // Minimum heart rate
    public float maxHeartRate = 180f; // Maximum heart rate

    void Update()
    {
        // Ensure the PPGMetricsStream and slider are assigned
        if (ppgMetricsStream != null && heartRateSlider != null)
        {
            // Get the latest heart rate reading
            float heartRate = GetLatestHeartRate();

            // Normalize the heart rate to a 0-1 range
            float normalizedHeartRate = NormalizeHeartRate(heartRate);

            // Set the slider value
            heartRateSlider.value = normalizedHeartRate;
        }
    }

    private float GetLatestHeartRate()
    {
        // Retrieve the heart rate buffer data
        float[] heartRateData = ppgMetricsStream.GetHeartRateData();

        // Return the latest value (the last entry in the buffer)
        return heartRateData.Length > 0 ? heartRateData[heartRateData.Length - 1] : 0f;
    }

    private float NormalizeHeartRate(float heartRate)
    {
        // Normalize the heart rate to a range of 0 to 1
        return Mathf.Clamp01((heartRate - minHeartRate) / (maxHeartRate - minHeartRate));
    }
}
