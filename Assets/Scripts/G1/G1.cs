using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class G1 : MonoBehaviour
{
    public TextMeshProUGUI output;
    public InputField textInputField;
    public TMP_InputField numberTMPInput;
    public TextMeshProUGUI sortedOutput;
    private string inputString;
    
    public List<int> intList = new List<int>();
    public List<int> sortedList = new List<int>();



    public void ButtonInputted()
    {
        
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

        sortedList = BubbleSort(intList);
        
        output.text = inputString;
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
    }

    static List<int> BubbleSort(List<int> list)
    {
        List<int> sortedList = new List<int>(list); 
        int size = sortedList.Count;
        bool swapped;

        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (sortedList[i - 1] > sortedList[i])
                {
                    // Swap elements
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;

                    swapped = true;
                }
            }

            // After each pass, the largest element will be at the end, so reduce the range
            size--;
        } while (swapped);

        return sortedList;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
