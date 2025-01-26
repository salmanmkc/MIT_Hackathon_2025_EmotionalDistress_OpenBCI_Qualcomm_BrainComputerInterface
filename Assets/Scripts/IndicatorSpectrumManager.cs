using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using OpenAI;

public class IndicatorSpectrumManager : MonoBehaviour
{
    [Header("UI Components")]
    public Slider indicatorSlider;
    public TMP_Text indicatorText;
    public string indicatorLabel;
    public Image sliderHandleImage; // Handle image to change dynamically

    [Header("Thresholds")]
    public float lowValue = 0.3f; // Threshold for low state
    public float midValue = 0.7f; // Threshold for mid state

    [Header("Handle Images")]
    public Image lowLabelImage;  // Image for low state
    public Image midLabelImage;  // Image for mid state
    public Image highLabelImage; // Image for high state

    [Header("States")]
    public string currentIndicatorLabel;
    [Range(0f, 1f)]
    public float currentIndicatorSliderValue; // Slider value exposed in the Inspector
    public bool lowState;
    public bool midState;
    public bool highState;

 void Start()
    {
        // Set the label text
        if (indicatorText != null)
        {
            //indicatorText.text = indicatorLabel;
        }

        // Initialize the handle image
        UpdateSliderState();
    }

    void Update()
    {
        UpdateSliderState();
        indicatorSlider.value = currentIndicatorSliderValue; 
        indicatorText.text = currentIndicatorLabel;
    }

    private void UpdateSliderState()
    {
        float sliderValue = indicatorSlider.value;

        // Check the slider value and set states accordingly
        if (sliderValue <= lowValue)
        {
            SetState(true, false, false);
            ChangeHandleImage(lowLabelImage.sprite);
            currentIndicatorLabel = "low";
        }
        else if (sliderValue > lowValue && sliderValue < midValue)
        {
            SetState(false, true, false);
            ChangeHandleImage(midLabelImage.sprite);
            currentIndicatorLabel = "mid";
        }
        else if (sliderValue >= midValue)
        {
            SetState(false, false, true);
            ChangeHandleImage(highLabelImage.sprite);
            currentIndicatorLabel = "high";
        }
    }

    private void SetState(bool isLow, bool isMid, bool isHigh)
    {
        // Update the state booleans
        lowState = isLow;
        midState = isMid;
        highState = isHigh;

        // Debug logs for testing
        Debug.Log($"{indicatorLabel} - Low: {lowState}, Mid: {midState}, High: {highState}");
    }

    private void ChangeHandleImage(Sprite newImage)
    {
        // Change the slider handle's image
        if (sliderHandleImage != null && newImage != null)
        {
            sliderHandleImage.sprite = newImage;
        }
    }
}
