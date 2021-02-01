using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private Slider _healthTreeSlider;
    [SerializeField] private ChristmasActionHandler _christmasAction;

    private void OnEnable() 
    {
        _christmasAction.CurrentChristmasHealth += UpdateChristmasHealthSlider;
        _healthTreeSlider.maxValue = _christmasAction.healthTree;
    }

    private void OnDisable() 
    {
        _christmasAction.CurrentChristmasHealth -= UpdateChristmasHealthSlider;
    }

    private void UpdateChristmasHealthSlider(int value)
    {
        _healthTreeSlider.value =  value;
    }
}
