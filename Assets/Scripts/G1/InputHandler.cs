using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InputHandler : MonoBehaviour
{
    public TextMeshProUGUI output;
    public InputField textInputField;
    public TMP_InputField numberTMPInput;
    public TextMeshProUGUI sortedOutput;
    public TextMeshProUGUI questionBox;
    public InputField answerBox;
    public GameObject questionObject;
    public GameObject answerObject;
    private string inputString;
    public CubeGenerator cubeGenerator;
    public GameObject prismObject;

    public List<int> intList = new List<int>();
    public List<int> sortedList = new List<int>();
    private void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
    }
    public void Trigger(GameObject gameObject)
    {
        if (gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
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
        List<int> prevPassList = new List<int>();
        int size = sortedList.Count;
        bool swapped;
        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = new List<GameObject>();
        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (sortedList[i - 1] > sortedList[i])
                {
                    prevPassList = sortedList;
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;

                    // After each pass, destroy the previously instantiated cubes from the enter button and instantiate new ones
                    cubeGenerator.grabandDestroyCubes();
                    cubeGenerator.InstantiateCubes(sortedList);

                    cubeObjects = cubeGenerator.grabCubes();

                    
                    string sortedString = string.Join(",", sortedList);
                    sortedOutput.text = sortedString;
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
    IEnumerator SteppedObjectBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        List<int> prevPassList = new List<int>();
        int size = list.Count;
        bool swapped;
        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = new List<GameObject>();
        cubeObjects = cubeGenerator.grabCubes();

        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (cubeObjects[i - 1].name.CompareTo(cubeObjects[i].name) > 0)
                {
                    prevPassList = list;

                    // Swap the names of cubeObjects[i-1] and cubeObjects[i]
                    string tempName = cubeObjects[i - 1].name;
                    cubeObjects[i - 1].name = cubeObjects[i].name;
                    cubeObjects[i].name = tempName;

                    string sortedString = string.Join(",", cubeObjects.Select(cube => int.Parse(cube.name)));
                    sortedOutput.text = sortedString;
                    cubeGenerator.UpdateLabels(cubeObjects);
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
        onComplete?.Invoke(cubeObjects.Select(cube => int.Parse(cube.name)).ToList());
    }


    public void SortButton()
    {
        intList = cubeGenerator.grabCubeNames();

        StartCoroutine(SteppedBubbleSortCoroutine(intList, (sortedList) =>
        {
            // Handle the sorted list here
            output.text = inputString;
            string sortedString = string.Join(",", sortedList);
            sortedOutput.text = sortedString;
        }));
        sortedList.Clear();
    }
    public void SortObjectsButton()
    {
        List<int> cubeNames = cubeGenerator.grabCubeNames();

        StartCoroutine(SteppedObjectBubbleSortCoroutine(cubeNames, (sortedCubeNames) =>
        {
            // Handle the sorted list here
            output.text = "Sorting complete";
            string sortedString = string.Join(",", sortedCubeNames);
            sortedOutput.text = sortedString;
        }));
    }
    public void TeachSteppedBubbleSort(List<int> list)
    {
        List<int> sortedList = new List<int>(list);
        List<GameObject> cubeObjects = new List<GameObject>();
        int size = sortedList.Count;
        int correctAnswer;
        
        bool swapped;
        G1 g1 = new G1();
        List<int> ansSortedList = new List<int>();
        ansSortedList = g1.BubbleSort(list);
        prismObject = GameObject.Find("Prism");
        cubeObjects = cubeGenerator.grabCubes();
        TextMeshProUGUI questionTextMeshPro = questionBox.GetComponent<TextMeshProUGUI>();
        questionBox.text = "Perform Bubble Sort Starting from the Left";
        Trigger(questionObject);
        Trigger(answerObject);

        //if (cubeObjects.Count > 0)
        //{
        //    Pick a random index within the boundaries of the list
        //    int randomIndex = Random.Range(0, cubeObjects.Count);

        //    Get the GameObject at the random index
        //   highlightedObject = cubeObjects[randomIndex];

        //    Now 'highlightedObject' contains a randomly selected GameObject from the list
        //    Debug.Log("Highlighted Object Name: " + highlightedObject.name);

        //    Move the prism above the x-coordinate of the highlighted object
        //    MovePrismAboveHighlightedObject(highlightedObject);
        //}
        //else
        //{
        //    Debug.LogWarning("No cube objects found in the list.");
        //}

    }
    private void MovePrismAboveHighlightedObject(GameObject highlightedObject)
    {

        // Check if the highlightedObject is not null and the prismObject is assigned
        if (highlightedObject != null && prismObject != null)
        {
            prismObject.SetActive(true);
            // Get the x-coordinate of the highlighted object
            float xCoordinate = highlightedObject.transform.position.x;

            // Set the position of the prism above the x-coordinate of the highlighted object
            prismObject.transform.position = new Vector3(xCoordinate, prismObject.transform.position.y, prismObject.transform.position.z);
        }
        else
        {
            Debug.LogError("Highlighted object or prism object is null. Assign valid GameObjects in the inspector.");
        }
    }
    public void teachSortButton()
    {
        
        intList = cubeGenerator.grabCubeText();

        TeachSteppedBubbleSort(intList);
        // Handle the sorted list here
        output.text = inputString;
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
        
        sortedList.Clear();
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
