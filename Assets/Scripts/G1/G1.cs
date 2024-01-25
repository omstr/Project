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
    public InputHandler iHandler;
    private string inputString;
    public void buttonPress()
    {
        //iHandler = new InputHandler();
        //iHandler.ButtonInput();
    }
    

    
    public List<int> BubbleSort(List<int> list)
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
