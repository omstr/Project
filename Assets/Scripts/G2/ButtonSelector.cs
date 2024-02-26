using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    /// <summary>
    /// This class is so that I don't have to create a bajillion references to all the images in each button to update each individual image respective to each button
    /// Ze images are found in for each transform in CodeDisplay.cs
    /// 
    /// 
    /// being concise is cool after the headache of figuring it out
    /// </summary>

    public static Button selectedButton; 
    public Button[] questionButtons;
    private Button button; 


    private void Start()
    {
        foreach (Button button in questionButtons)
        {
            // Add a listener to the onClick event of each button
            button.onClick.AddListener(() => OnClick(button));
        }
    }

    private void OnClick(Button clickedButton)
    {
        selectedButton = clickedButton; // Store the clicked button as the selected button
        Debug.Log("the selected button is: " + selectedButton);
    }
}
