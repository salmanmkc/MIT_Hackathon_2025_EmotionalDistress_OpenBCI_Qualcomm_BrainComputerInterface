using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IndicatorSpectrumManager : MonoBehaviour
{

public Slider indicatorSlider;
public TMP_Text indicatorText;
public String indicatorLabel;
//public Image currentLable;
public GameObject lowLabel;
public float lowValue;
public GameObject midLabel;
public float midValue;
public GameObject HighLabel;
public float highValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indicatorText.text = indicatorLabel;
    }

    // Update is called once per frame
    void Update()
    {
        if (indicatorSlider.value<=lowValue)
            {
                lowLabel.SetActive(true);
                midLabel.SetActive(false);
                HighLabel.SetActive(false);
            }
        else if (indicatorSlider.value>lowValue&&indicatorSlider.value<midValue)
            {
                lowLabel.SetActive(false);
                midLabel.SetActive(true);
                HighLabel.SetActive(false);
            }
        else if (indicatorSlider.value>=midValue)
            {
                lowLabel.SetActive(false);
                midLabel.SetActive(false);
                HighLabel.SetActive(true);
            }
    }
}
