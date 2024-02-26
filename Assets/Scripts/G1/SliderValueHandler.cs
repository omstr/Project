using UnityEngine;
using UnityEngine.UI;

public class SliderValueHandler : MonoBehaviour
{
    public Slider delaySlider;

    // Flag to keep track of whether the Slider is currently incrementing from 0.1 to 1 or decrementing from 1 to 0.1
    private bool isIncrementingToOne = true;

    void Start()
    {
        // Subscribe to the Slider's value changed event
        delaySlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Method to handle Slider value changes
    void OnSliderValueChanged(float value)
    {
        // If the value is 1 and currently incrementing to 1, toggle whole numbers off
        if (value == 1 && isIncrementingToOne)
        {
            delaySlider.wholeNumbers = false;
            isIncrementingToOne = false;
        }
        // If the value is 0.1 and currently decrementing to 0.1, toggle whole numbers on
        else if (value == 0.1 && !isIncrementingToOne)
        {
            delaySlider.wholeNumbers = true;
            isIncrementingToOne = true;
        }
    }
}
