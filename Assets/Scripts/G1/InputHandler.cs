using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float delayInSeconds = 1f;
    public CubeGenerator cubeGenerator;
    public GameObject prismObject;
    public Slider delaySlider;
    public List<int> intList = new List<int>();
    public List<int> sortedList = new List<int>();
    public Image tickImage;
    public Image crossImage;
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
        List<GameObject> cubeObjects = cubeGenerator.grabCubes();
        MovePrismAboveHighlightedObject(cubeObjects[0]);
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
        cubeObjects = cubeGenerator.grabCubes();
        MovePrismAboveHighlightedObject(cubeObjects[0]);
        float adjustedDelay = delayInSeconds * delaySlider.value;
        do
        {

            swapped = false;
            cubeObjects = cubeGenerator.grabCubes();

            for (int i = 1; i < size; i++)
            {

                cubeObjects = cubeGenerator.grabCubes();
                MovePrismAboveHighlightedObject(cubeObjects[i]);
                yield return new WaitForSeconds(adjustedDelay);

                if (sortedList[i - 1] > sortedList[i])
                {
                    MovePrismAboveHighlightedObject(cubeObjects[i]);

                    prevPassList = sortedList;
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;
                    //MovePrismAboveHighlightedObject(cubeObjects[i]);
                    //yield return new WaitForSeconds(0.5f);
                    // After each pass, destroy the previously instantiated cubes from the enter button and instantiate new ones
                    cubeGenerator.grabandDestroyCubes();
                    cubeGenerator.InstantiateCubes(sortedList);
                    
                    cubeObjects = cubeGenerator.grabCubes();

                    
                    string sortedString = string.Join(",", sortedList);
                    sortedOutput.text = sortedString;
                    
                    // Introduce a delay
                    yield return new WaitForSeconds(adjustedDelay);
                    swapped = true;
                    MovePrismAboveHighlightedObject(cubeObjects[i]);
                }
                else
                {
                    cubeObjects = cubeGenerator.grabCubes();
                    yield return new WaitForSeconds(1f);
                    MovePrismAboveHighlightedObject(cubeObjects[i]);

                }
                cubeObjects = cubeGenerator.grabCubes();
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
        MovePrismAboveHighlightedObject(cubeObjects[0]);
        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (cubeObjects[i - 1].name.CompareTo(cubeObjects[i].name) > 0)
                {
                    //prismObject.SetActive(true);
                    prevPassList = list;
                    MovePrismAboveHighlightedObject(cubeObjects[i-1]);
                    // Swap the names of cubeObjects[i-1] and cubeObjects[i]
                    string tempName = cubeObjects[i - 1].name;
                    cubeObjects[i - 1].name = cubeObjects[i].name;
                    cubeObjects[i].name = tempName;
                    MovePrismAboveHighlightedObject(cubeObjects[i]);

                    string sortedString = string.Join(",", cubeObjects.Select(cube => int.Parse(cube.name)));
                    sortedOutput.text = sortedString;
                    cubeGenerator.UpdateLabels(cubeObjects);
                    float adjustedDelay = delayInSeconds * delaySlider.value;
                    // Introduce a delay
                    yield return new WaitForSeconds(adjustedDelay);
                    swapped = true;
                    if(swapped == false)
                    {
                        MovePrismAboveHighlightedObject(cubeObjects[i+1]);
                    }
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
    public IEnumerator TeachSteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        List<int> sortedList = new List<int>(list);
        List<int> prevPassList = new List<int>();
        int size = sortedList.Count;
        bool swapped;
        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = new List<GameObject>();
        cubeObjects = cubeGenerator.grabCubes();

        List<int> cubeNames = new List<int>();
        MovePrismAboveHighlightedObject(cubeObjects[0]);
        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (sortedList[i - 1] > sortedList[i])
                {
                    prevPassList = sortedList;
                    MovePrismAboveHighlightedObject(cubeObjects[i - 1]);
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;
                    MovePrismAboveHighlightedObject(cubeObjects[i]);

                    
                    cubeObjects = cubeGenerator.grabCubes();


                    string sortedString = string.Join(",", sortedList);
                    sortedOutput.text = sortedString;
                    // Introduce a delay & wait for the drag & dropping
                    yield return StartCoroutine(WaitForUserSwap(cubeObjects));
                    cubeNames = cubeGenerator.grabCubeNames();
                    // Compare cubeNames to sortedList after each pass
                    if (ListsAreEqual(cubeNames, sortedList))
                    {
                        Debug.Log("The list of the cube names" + cubeNames.ToString());
                        Debug.Log("The sorted list from the bubble sort" + sortedList.ToString());
                        tickImage.enabled = true;
                        SpawnImageOverCube(cubeObjects[i]);
                        output.text = "Correct";
                    }
                    else
                    {
                        Debug.Log("The list of the cube names" + cubeNames);
                        Debug.Log("The sorted list from the bubble sort" + sortedList);
                        SpawnImageOverCube(cubeObjects[i]);
                        crossImage.enabled = true;
                        output.text = "Incorrect";
                    }
                    swapped = true;
                }
            }



            // Reduce the range for the next pass
            size--;

        } while (swapped && size > 1);

        Debug.Log("Sorting Complete");

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);

    }
    private void SpawnImageOverCube(GameObject cube)
    {
        // Spawn the tickImage over the cube's position
        Transform game1Transform = transform.parent.Find("Game1");

        Instantiate(tickImage, cube.transform.position, Quaternion.identity);

        Debug.Log("Spawned tick image at: " + tickImage.transform.position);
        Debug.Log("Cube position: " + cube.transform.position);
    }
    private bool ListsAreEqual(List<int> list1, List<int> list2)
    {
        // Check if two lists are equal
        return list1.SequenceEqual(list2);
    }

    private IEnumerator WaitForUserSwap(List<GameObject> cubeObjects)
    {
        bool userSwapped = false;

        List<int> initialNames = cubeObjects.Select(cube => int.Parse(cube.name)).ToList();

        List<bool> cubeDraggedFlags = new List<bool>(cubeObjects.Count);

        // Initialize flags to false
        for (int i = 0; i < cubeObjects.Count; i++)
        {
            cubeDraggedFlags.Add(false);
        }
        // Set the cubes to be draggable
        foreach (var cube in cubeObjects)
        {
            var cubeMovement = cube.GetComponent<CubeMovement>();
            if (cubeMovement != null)
            {
                cubeMovement.EnableDrag();
            }
        }

        // Wait before checking changes
        yield return new WaitForSeconds(0.1f);

        // Continue checking for changes until two cubes have been dragged
        while (!userSwapped)
        {
            // Wait for the next frame to avoid performance issues
            yield return null;

            // Check if at least two cubes have been dragged
            for (int i = 0; i < cubeObjects.Count; i++)
            {
                if (!cubeDraggedFlags[i])
                {
                    // If the cube hasn't been dragged, check if its name has changed
                    if (int.Parse(cubeObjects[i].name) != initialNames[i])
                    {
                        cubeDraggedFlags[i] = true;
                    }
                }
            }

            // Check if at least two cubes have been dragged
            if (cubeDraggedFlags.Count(flag => flag) >= 2)
            {
                userSwapped = true;
            }
        }

        //disable cube dragging
        foreach (var cube in cubeObjects)
        {
            var cubeMovement = cube.GetComponent<CubeMovement>();
            if (cubeMovement != null)
            {
                cubeMovement.DisableDrag();
            }
            
        }

        if (userSwapped)
        {
            Debug.Log("User has completed the swap");
        }
        else
        {
            Debug.Log("User has not completed the swap");
        }
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

        //TeachSteppedBubbleSort(intList);
        //// Handle the sorted list here
        //output.text = inputString;
        //string sortedString = string.Join(",", sortedList);
        //sortedOutput.text = sortedString;
        
        //sortedList.Clear();

        List<int> cubeNames = cubeGenerator.grabCubeNames();

        StartCoroutine(TeachSteppedBubbleSortCoroutine(cubeNames, (sortedCubeNames) =>
        {
            // Handle the sorted list here
            output.text = "Teaching? complete";
            string sortedString = string.Join(",", sortedCubeNames);
            sortedOutput.text = sortedString;
        }));
    }
    public void ReturnToPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");

    }
}
