using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GuiSettingsManager : MonoBehaviour
{
    [SerializeField] private GolfClubSetting _golfClubSetting;

    [SerializeField] private Slider _slider;
    //TODO: create a list (or key-value directory) of the Sliders, and iterate over to add and remove the listeners 

    void OnEnable()
    {
      _slider.onValueChanged.AddListener(UpdateSliderValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateSliderValue);
    }

    private void UpdateSliderValue(float slideValue)
    {
        _golfClubSetting.SmoothPositionAmount = slideValue;
    }
}
