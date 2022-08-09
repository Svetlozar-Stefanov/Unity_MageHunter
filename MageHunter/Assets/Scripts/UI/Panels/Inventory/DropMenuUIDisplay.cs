using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropMenuUIDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField inputField;
    public void SetUp(int maxAmount)
    {
        int minAmmount = 1;
        slider.minValue = minAmmount;
        slider.value = minAmmount;
        slider.maxValue = maxAmount;
        inputField.text = minAmmount.ToString();
    }

    public void HandleOnInputFieldValueChanged(TMP_InputField inputField)
    {
        int val;
        if (!int.TryParse(inputField.text, out val))
        {
            return;
        }
        
        if (val <= 0)
        {
            return;
        }

        if (val > slider.maxValue)
        {
            slider.value = slider.maxValue;
            inputField.text = slider.maxValue.ToString();
            return;
        }

        slider.value = val;
    }

    public void HandleOnSliderValueChanged()
    {
        string val = slider.value.ToString();
        inputField.text = val;
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
