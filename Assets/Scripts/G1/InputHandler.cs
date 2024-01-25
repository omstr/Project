using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public TextMeshProUGUI output;
    public InputField textInputField;
    public TMP_InputField numberTMPInput;
    public TextMeshProUGUI sortedOutput;
    private string inputString;
    public CubeGenerator cubeGenerator;

    public List<int> intList = new List<int>();
    public List<int> sortedList = new List<int>();
    private void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
    }
    public void ButtonInputted()
    {
        G1 g1 = new G1();
        inputString = textInputField.text;
        Debug.Log(inputString);
        //for each character in the string convert it to an integer and add it to an array
        // Split the string by commas
        string[] values = inputString.Split(',');

        // Convert and add each value to the List<int>
        foreach (string value in values)
        {
            if (int.TryParse(value, out int intValue))
            {
                intList.Add(intValue);
            }
            else
            {
                Debug.Log($"Unable to convert '{value}' to int.");
            }
        }

        // Print the contents of the List<int>
        foreach (int value in intList)
        {
            Debug.Log(value);
        }

        sortedList = g1.BubbleSort(intList);

        output.text = inputString;
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
        cubeGenerator.InstantiateCubes(intList);
    }
}
