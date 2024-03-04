using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatImageDisplay : MonoBehaviour
{
   
    public static void UpdateSuccessRate(float successRate, TextMeshProUGUI successRateText, Image background)
    {
        // Set the text value
        successRateText.text = successRate.ToString("0.##") + "%";
        Color currentColor = background.color;
        currentColor.a = 128;
        background.color = currentColor;
        // Set the text color based on the success rate
        if (successRate < 40)
        {
            
            background.color = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red
        }
        else if (successRate < 75)
        {
            
            background.color = new Color(1f, 0.5f, 0f, 0.5f); // Semi-transparent orange
        }
        else
        {
            
            background.color = new Color(0f, 1f, 0f, 0.5f); // Semi-transparent green
        }
    }
}
