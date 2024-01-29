using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingController : MonoBehaviour
{
    public Slider sortingSlider;
    public InputHandler iHandler;

    // Start is called before the first frame update
    private void Start()
    {
        sortingSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnSliderValueChanged(float value)
    {
        // Assuming you have a method to perform sorting logic
        // and accept the current iteration or progress as a parameter
        int currentIteration = Mathf.RoundToInt(value);
        PerformSortingLogic(currentIteration);
    }

    private void PerformSortingLogic(int currentIteration)
    {
        // Ensure the InputHandler instance is not null
        if (iHandler != null)
        {
            iHandler.SortButton(currentIteration);
        }
        else
        {
            Debug.LogError("InputHandler instance is null in SortingController.");
        }
    }
}
