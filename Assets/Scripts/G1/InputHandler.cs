using System;
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
    public List<int> handleInput()
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
        //foreach (int value in intList)
        //{
        //    Debug.Log(value);
        //}
        return intList;
    }
    public void enterButton()
    {
        
        //cubeGenerator.grabandDestroyCubes();
        intList = this.handleInput();
        G1 g1 = new G1();
        sortedList = g1.BubbleSort(intList);

        output.text = inputString;
        
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
        cubeGenerator.InstantiateCubes(intList);
        sortedList.Clear();
        intList.Clear();
    }
    public void clearButton()
    {
        Transform game1Transform = transform.parent.Find("Game1");

        if (game1Transform != null)
        {
            // Ensure the cubeGenTransform is found under game1Transform
            Transform cubeGenTransform = game1Transform.Find("CubeGen");

            if (cubeGenTransform != null)
            {
                // Iterate through children of "CubeGen"
                foreach (Transform child in cubeGenTransform)
                {
                    // Check if the child has the specified tag
                    if (child.CompareTag("CubeTag"))
                    {
                        // Destroy the child object (cube)
                        Destroy(child.gameObject);
                    }
                }
            }
            else
            {
                Debug.LogError("Could not find CubeGen transform under Game1.");
            }
        }
        else
        {
            Debug.LogError("Could not find Game1 transform.");
        }
    }
    public void StartSteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        StartCoroutine(SteppedBubbleSortCoroutine(list, onComplete));
    }
    IEnumerator SteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
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
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;

                    // After each pass, destroy the previously instantiated cubes from the enter button and instantiate new ones
                    Debug.Log("Before destroying cubes. CubeGenerator: " + cubeGenerator);
                    cubeGenerator.grabandDestroyCubes();
                    Debug.Log("After destroying cubes.");
                    Debug.Log("Before instantiating cubes. CubeGenerator: " + cubeGenerator);
                    cubeGenerator.InstantiateCubes(sortedList);
                    Debug.Log("After instantiating cubes.");
                    string sortedString = string.Join(",", sortedList);

                    // Introduce a delay
                    yield return new WaitForSeconds(1f);
                    swapped = true;
                }
            }

            

            // Reduce the range for the next pass
            size--;

        } while (swapped);

        Debug.Log("Sorting Complete");

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);
    }

    public void SortButton(int currentIteration)
    {
        intList = this.grabCubeText();

        StartCoroutine(SteppedBubbleSortCoroutine(intList, (sortedList) =>
        {
            // Handle the sorted list here
            output.text = inputString;
            string sortedString = string.Join(",", sortedList);
            sortedOutput.text = sortedString;
        }));
        sortedList.Clear();
    }
    public List<GameObject> grabCubes() 
    {
        Transform game1Transform = transform.parent.Find("Game1");

        if (game1Transform != null)
        {
            // Ensure the cubeGenTransform is found under game1Transform
            Transform cubeGenTransform = game1Transform.Find("CubeGen");

            if (cubeGenTransform != null)
            {
                List<GameObject> cubeObjects = new List<GameObject>();

                // Iterate through children of "CubeGen"
                foreach (Transform child in cubeGenTransform)
                {
                    // Check if the child has the specified tag
                    if (child.CompareTag("CubeTag"))
                    {
                        cubeObjects.Add(child.gameObject);
                        
                    }
                }
                return cubeObjects;

                // ... rest of the code remains the same
            }
            else
            {
                Debug.LogError("Could not find CubeGen transform under Game1.");
            }
        }
        else
        {
            Debug.LogError("Could not find Game1 transform.");
        }
        return null;
        
    }
    public List<int> grabCubeText()
    {
        Transform game1Transform = transform.parent.Find("Game1");

        if (game1Transform != null)
        {
            // Ensure the cubeGenTransform is found under game1Transform
            Transform cubeGenTransform = game1Transform.Find("CubeGen");

            if (cubeGenTransform != null)
            {
                List<int> labelTexts = new List<int>();

                // Iterate through children of "CubeGen"
                foreach (Transform child in cubeGenTransform)
                {
                    
                    if (child.CompareTag("CubeTag"))
                    {
                        // TextMeshProUGUI component is directly on the child object
                        TextMeshProUGUI textMeshPro = child.GetComponentInChildren<TextMeshProUGUI>();

                        if (textMeshPro != null)
                        {
                            // parse the text to an integer
                            if (int.TryParse(textMeshPro.text, out int labelValue))
                            {
                                labelTexts.Add(labelValue);
                            }
                            else
                            {
                                Debug.LogError("Failed to parse label text to int: " + textMeshPro.text);
                            }
                        }
                        else
                        {
                            Debug.LogError("TextMeshProUGUI component not found on the cube.");
                        }
                    }
                }

                return labelTexts;
            }
            else
            {
                Debug.LogError("Could not find CubeGen transform under Game1.");
            }
        }
        else
        {
            Debug.LogError("Could not find Game1 transform.");
        }

        return null;
    }
    /* Old sort before coroutine 
    public void SortButton()
    {
        intList = this.grabCubeText();
        
        G1 g1 = new G1();

        //cubeGenerator.InstantiateCubes(intList);
        float delay = 1;
        sortedList = this.SteppedBubbleSort(intList);
        

        output.text = inputString;

        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;

        
        //StartCoroutine(SortedCoroutine(intList));

    }*/
    /*
    IEnumerator SortedCoroutine(List<int> inputList)
    {
        G1 g1 = new G1();
        List<int> sortedList = new List<int>(inputList);

        foreach (int value in this.SteppedBubbleSort(inputList))
        {
            Debug.Log("the value in the stepped bubble sort is: " + value);

            // Update UI or perform other operations as needed
            string sortString = string.Join(",", sortedList);
            sortedOutput.text = sortString;
            cubeGenerator.InstantiateCubes(inputList);

            // Introduce a delay of 1 second
            yield return new WaitForSeconds(2f);
        }

        // Sorting is complete, handle the final results
        output.text = string.Join(",", inputList);
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;

        cubeGenerator.InstantiateCubes(inputList);
    }
    */
}
