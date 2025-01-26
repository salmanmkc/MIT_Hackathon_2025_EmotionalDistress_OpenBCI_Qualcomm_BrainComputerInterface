using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.UI;

public class BandPowerManager : MonoBehaviour
{
    public PPGMetricsStream metricsStream;
    public AverageBandPowerStream bandPowerStream;
    public Slider brainWaveSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float sumPowers;
    [Range(0f, 1f)]
    public float power;
    public float multiplier;

    // ppg current values
    public float hrValue;
    public float hrvValue;
    public float boValue;


    // Brain wave current values
    public float alphaValue;
    public float betaValue;
    public float gammaValue;
    public float deltaValue;
    public float thetaValue;

    // Brain wave ranges
    private readonly float alphaMin = 0f, alphaMax = 0.4f;
    private readonly float betaMin = 0f, betaMax = 0.3f;
    private readonly float gammaMin = 0f, gammaMax = 0.2f;
    private readonly float deltaMin = 0f, deltaMax = 0.2f;
    private readonly float thetaMin = 0f, thetaMax = 1f;

    // Slider to display the normalized value

    [Range(0f, 1f)]
    public float lungSlider;
    [Header("Heart Rate Range")]
    public bool isSimulationHeart;
    public float minHeartRate = 40f; // Minimum heart rate
    public float maxHeartRate = 180f; // Maximum heart rate

    [Range(0f, 1f)]
    public float heartRateSlider;
    public Slider hRSlider;
    //public Slider heartRateSlider;


    [Header("Heart Rate Zones")]
    public float greenMin = 50f; // Minimum heart rate for Green Zone
    public float greenMax = 115f; // Maximum heart rate for Green Zone
    public float yellowMax = 150f; // Maximum heart rate for Yellow Zone
    public float redMin = 150f; // Minimum heart rate for Red Zone

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSimulationHeart)
        {
                    // Example: Simulate a heart rate value for testing
        float simulatedHeartRate = Random.Range(40f, 180f);
        UpdateHeartRate(simulatedHeartRate);

        }
        //sumPowers = (bandPowerStream.AverageBandPower.Alpha + bandPowerStream.AverageBandPower.Beta + bandPowerStream.AverageBandPower.Gamma + bandPowerStream.AverageBandPower.Delta + bandPowerStream.AverageBandPower.Theta) / 5;
        //power = sumPowers * multiplier;
        alphaValue = bandPowerStream.AverageBandPower.Alpha;
        betaValue = bandPowerStream.AverageBandPower.Beta;
        gammaValue = bandPowerStream.AverageBandPower.Gamma;
        deltaValue = bandPowerStream.AverageBandPower.Delta;
        thetaValue = bandPowerStream.AverageBandPower.Theta;


        float[] hrValues = metricsStream.GetHeartRateData();
        var sum = 0f;
        for (int i = 0; i < hrValues.Length; i++)
        {
            sum += hrValues[i];
        }
        hrvValue = sum / hrValues.Length;




        // hrvValue =
        // boValue = 

        // Compute the normalized average
        float normalizedAverage = ComputeNormalizedAverage();

        // Set the slider value
        if (brainWaveSlider != null)
        {
            brainWaveSlider.value = normalizedAverage;
            power = brainWaveSlider.value;

        }else{
            power = normalizedAverage;
        }

        // Ensure the PPGMetricsStream and slider are assigned
        if (metricsStream != null && heartRateSlider != null)
        {
            // Get the latest heart rate reading
            float heartRate = GetLatestHeartRate();

            // Normalize the heart rate to a 0-1 range
            float normalizedHeartRate = NormalizeHeartRate(heartRate);

            // Set the slider value
            if (!isSimulationHeart)
            {
            heartRateSlider = normalizedHeartRate;
            hrValue = normalizedHeartRate;
            }
            if(hRSlider!=null)
            {
                hRSlider.value = normalizedHeartRate;
            }

        }

    }

    private float ComputeNormalizedAverage()
    {
        // Normalize each brain wave value
        float normalizedAlpha = Normalize(alphaValue, alphaMin, alphaMax);
        float normalizedBeta = Normalize(betaValue, betaMin, betaMax);
        float normalizedGamma = Normalize(gammaValue, gammaMin, gammaMax);
        float normalizedDelta = Normalize(deltaValue, deltaMin, deltaMax);
        float normalizedTheta = Normalize(thetaValue, thetaMin, thetaMax);

        // Calculate the average of the normalized values
        float normalizedAverage = (normalizedAlpha + normalizedBeta + normalizedGamma + normalizedDelta + normalizedTheta) / 5f;

        return normalizedAverage;
    }

    private float Normalize(float value, float min, float max)
    {
        // Avoid division by zero in case of improper configuration
        if (max - min == 0)
            return 0;

        // Clamp the value within the range [min, max] and normalize
        return Mathf.Clamp01((value - min) / (max - min));
    }

    private float GetLatestHeartRate()
    {
        // Retrieve the heart rate buffer data
        float[] heartRateData = metricsStream.GetHeartRateData();

        // Return the latest value (the last entry in the buffer)
        return heartRateData.Length > 0 ? heartRateData[heartRateData.Length - 1] : 0f;
    }

    private float NormalizeHeartRate(float heartRate)
    {
        // Normalize the heart rate to a range of 0 to 1
        return Mathf.Clamp01((heartRate - minHeartRate) / (maxHeartRate - minHeartRate));

        //  if (heartRate < greenMin)
        // {
        //     // Below minimum heart rate, map to 0
        //     return 0f;
        // }
        // else if (heartRate <= greenMax)
        // {
        //     // Green Zone: Map to 0–0.33
        //     return Mathf.Lerp(0f, 0.33f, (heartRate - greenMin) / (greenMax - greenMin));
        // }
        // else if (heartRate <= yellowMax)
        // {
        //     // Yellow Zone: Map to 0.33–0.69
        //     return Mathf.Lerp(0.33f, 0.69f, (heartRate - greenMax) / (yellowMax - greenMax));
        // }
        // else
        // {
        //     // Red Zone: Map to 0.69–1
        //     return Mathf.Lerp(0.69f, 1f, (heartRate - redMin) / (yellowMax - redMin));
        // }

    }

    public void UpdateHeartRate(float heartRate)
    {
        // Normalize the heart rate to the slider range [0, 1]
        float normalizedHeartRate = NormalizeHeartRate(heartRate);


            heartRateSlider = normalizedHeartRate;
            hrValue = normalizedHeartRate;
            if(hRSlider!=null)
            {
                hRSlider.value = normalizedHeartRate;

            }

    }

}
