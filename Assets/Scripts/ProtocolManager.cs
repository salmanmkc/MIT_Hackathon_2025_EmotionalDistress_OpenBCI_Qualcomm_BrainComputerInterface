using OpenAI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

[System.Serializable]
public class ProtocolCriteria
{
    public string label;
    // Boolean states for the heart indicator
    public bool heartLow;
    public bool heartMid;
    public bool heartHigh;

    // Boolean states for the brain indicator
    public bool brainLow;
    public bool brainMid;
    public bool brainHigh;

    // Boolean states for the lung indicator
    public bool lungLow;
    public bool lungMid;
    public bool lungHigh;

    public UnityEvent protocolEvent;
}

public class ProtocolManager : MonoBehaviour
{
     [Header("Indicators")]
    public IndicatorSpectrumManager heartIndicator;
    
    public IndicatorSpectrumManager brainIndicator;
    public IndicatorSpectrumManager lungIndicator;

   [Header("Protocol Settings")]    
   public PlayableDirector protocolTimeline;

    public ProtocolCriteria[] protocolCriterias;
    public UnityEvent panicProtocolEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
        [Header("Editor Control")]
        public BandPowerManager openBCI;
        
        
        public bool isSimulationMode;
    [Range(0f, 1f)]
    public float heartSliderValue; // Slider value exposed in the Inspector
    public string currentHeartLabel;
    [Range(0f, 1f)]
    public float brainSliderValue; // Slider value exposed in the Inspector
    public string currentBrainLabel;
    [Range(0f, 1f)]
    public float lungSliderValue; // Slider value exposed in the Inspector
    public string currentLungLabel;

    void Start()
    {
        CheckAndTriggerProtocols();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSimulationMode==true)
        {
            heartIndicator.currentIndicatorSliderValue = heartSliderValue;
            brainIndicator.currentIndicatorSliderValue = brainSliderValue;
            lungIndicator.currentIndicatorSliderValue = lungSliderValue;
            currentHeartLabel = heartIndicator.currentIndicatorLabel;
            currentBrainLabel = brainIndicator.currentIndicatorLabel;
            currentLungLabel = lungIndicator.currentIndicatorLabel;
        }

        UpdateProtocolStates();
        CheckAndTriggerProtocols();
        UpdateCriteriaLabels();

        if(openBCI!=null)
        {
            //heartSliderValue=openBCI.hRSlider.value;
            heartIndicator.currentIndicatorSliderValue=openBCI.heartRateSlider;
//            brainSliderValue=openBCI.brainWaveSlider.value;
            brainIndicator.currentIndicatorSliderValue=openBCI.power;
            lungIndicator.currentIndicatorSliderValue = openBCI.lungSlider;
        }
    }

public void InitiatePanicProtocol()
{
    Debug.Log("Panic Protocol Triggered!");
    //protocolTimeline.Play();
    panicProtocolEvent?.Invoke();
}

        private void CheckAndTriggerProtocols()
    {
        foreach (var criteria in protocolCriterias)
        {
            // Check if all criteria for the current protocol match the indicator states
            if (MatchesCriteria(criteria))
            {
                criteria.protocolEvent?.Invoke(); // Trigger the associated Unity event
                Debug.Log($"Triggered Protocol: {criteria.label}");
            }
        }
    }
    private bool MatchesCriteria(ProtocolCriteria criteria)
    {
        // Compare the states of each indicator with the protocol criteria
        return
            (criteria.heartLow == heartIndicator.lowState &&
             criteria.heartMid == heartIndicator.midState &&
             criteria.heartHigh == heartIndicator.highState) &&
            (criteria.brainLow == brainIndicator.lowState &&
             criteria.brainMid == brainIndicator.midState &&
             criteria.brainHigh == brainIndicator.highState) &&
            (criteria.lungLow == lungIndicator.lowState &&
             criteria.lungMid == lungIndicator.midState &&
             criteria.lungHigh == lungIndicator.highState);
    }
    public void UpdateProtocolStates()
    {
        foreach (var criteria in protocolCriterias)
        {
            // Check heart states and update the label
            if (criteria.heartLow)
            {
                heartIndicator.currentIndicatorLabel = "low";
                currentHeartLabel = heartIndicator.currentIndicatorLabel;
            }
            else if (criteria.heartMid)
            {
                heartIndicator.currentIndicatorLabel = "mid";
                currentHeartLabel = heartIndicator.currentIndicatorLabel;
            }
            else if (criteria.heartHigh)
            {
                heartIndicator.currentIndicatorLabel = "high";
                currentHeartLabel = heartIndicator.currentIndicatorLabel;
            }

            // Check brain states and update the label
            if (criteria.brainLow)
            {
                brainIndicator.currentIndicatorLabel = "low";
                currentBrainLabel = brainIndicator.currentIndicatorLabel;
            }
            else if (criteria.brainMid)
            {
                brainIndicator.currentIndicatorLabel = "mid";
                currentBrainLabel = brainIndicator.currentIndicatorLabel;
            }
            else if (criteria.brainHigh)
            {
                brainIndicator.currentIndicatorLabel = "high";
                currentBrainLabel = brainIndicator.currentIndicatorLabel;
            }

            // Check lung states and update the label
            if (criteria.lungLow)
            {
                lungIndicator.currentIndicatorLabel = "low";
                currentLungLabel = lungIndicator.currentIndicatorLabel;
            }
            else if (criteria.lungMid)
            {
                lungIndicator.currentIndicatorLabel = "mid";
                currentLungLabel = lungIndicator.currentIndicatorLabel;
            }
            else if (criteria.lungHigh)
            {
                lungIndicator.currentIndicatorLabel = "high";
                currentLungLabel = lungIndicator.currentIndicatorLabel;
            }

        }
    }

    public void ActivateProtocol()
    {
        foreach (var ac in protocolCriterias)
        {

        }
    }

    public void UpdateCriteriaLabels()
    {
        currentHeartLabel = heartIndicator.currentIndicatorLabel;
        currentBrainLabel = brainIndicator.currentIndicatorLabel;
        currentLungLabel = lungIndicator.currentIndicatorLabel;
    }
}
