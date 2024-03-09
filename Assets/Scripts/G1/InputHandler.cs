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
    public Canvas canvas;
    public TextMeshProUGUI output;
    public InputField textInputField;
    public TMP_InputField numberTMPInput;
    public TextMeshProUGUI sortedOutput;
    public TextMeshProUGUI questionBox;
    public TextMeshProUGUI scoreLabel;
    public Button enterButton;
    public InputField answerBox;
    public GameObject questionObject;
    public GameObject answerObject;
    private string inputString;
    private float delayInSeconds = 1f;
    public CubeGenerator cubeGenerator;
    private GameObject prismObject;
    public Slider delaySlider;
    public List<int> intList = new List<int>();
    public List<int> sortedList = new List<int>();
    public Image tickImage;
    public Image crossImage;
    public int maxNumbers = 9;
    public TextMeshProUGUI questionLabel;

    private RectTransform CubeGenTransform;

    private bool isSorting = false;
   
    private Coroutine clearCoroutine;
    private Coroutine sortingCoroutine;

    private void Awake()
    {
        questionLabel = transform.Find("QuestionLabel").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        prismObject = transform.Find("Prism").GetComponent<GameObject>();

        CubeGenTransform = transform.Find("CubeGen").GetComponent<RectTransform>();
        textInputField.onEndEdit.AddListener(ValidateInput);

        delaySlider.minValue = 0.1f;
        delaySlider.value = 1;
        


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
    public void ValidateInput(string text)
    {
        // Validate the input text
        // Allow digits (0-9) and comma (,) characters only
        string validText = "";
        int commaCount = 0;

        foreach (char c in text)
        {
            if (char.IsDigit(c) || c == ',')
            {
                if (c == ',')
                {
                    commaCount++;
                }

                if (commaCount <= maxNumbers - 1 || (commaCount == maxNumbers && c == ','))
                {
                    validText += c;
                }
            }
        }

        // Update the input field text with the valid text
        textInputField.text = validText;
    }
    public void EnterButton()
    {
        
        //cubeGenerator.grabandDestroyCubes();
        intList = this.handleInput();

        Game1 game1 = new Game1();

        sortedList = game1.BubbleSort(intList);
        output.text = inputString;
        
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
        Clear(() =>
        {
            
            // this code will be executed after the clear coroutine finishes
            cubeGenerator.InstantiateCubes(intList);

        });
        

        List<GameObject> cubeObjects = cubeGenerator.grabCubes();

        //MovePrismAboveHighlightedObject(cubeObjects[0]);
        //sortedList.Clear();
        //intList.Clear();
    }
    public void StopButton()
    {
        StopCoroutine(clearCoroutine);
        StopCoroutine(sortingCoroutine);
        //StopCoroutine(teachingCoroutine);
    }

    public void Clear(Action callback)
    {
        // Stop the clear coroutine if it's running
        if (clearCoroutine != null)
        {
            StopCoroutine(clearCoroutine);
        }

        // start the clear coroutine
        clearCoroutine = StartCoroutine(ClearCoroutine(() =>
        {
            // after clearing is complete, invoke the callback to start cube instantiation
            callback?.Invoke();
        }));
    }
    private IEnumerator ClearCoroutine(Action callback)
    {
        // wait for one frame to ensure all objects are destroyed
        yield return null;

        
        
      
            // Ensure the cubeGenTransform is found under game1Transform
            Transform cubeGenTransform = transform.Find("CubeGen");

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

        yield return new WaitForSeconds(0.1f);
        callback?.Invoke();

    }
    public void SwapCubes(GameObject cube1, GameObject cube2)
    {
        // Get the X positions of the cubes
        float tempX = cube1.transform.localPosition.x;
        float otherX = cube2.transform.localPosition.x;

        // Swap the X positions
        cube1.transform.localPosition = new Vector3(otherX, cube1.transform.localPosition.y, cube1.transform.localPosition.z);
        cube2.transform.localPosition = new Vector3(tempX, cube2.transform.localPosition.y, cube2.transform.localPosition.z);



        // Get the index of each cube in the list
        int index1 = cube1.transform.GetSiblingIndex();
        int index2 = cube2.transform.GetSiblingIndex();

        // Swap their positions in the list
        cube1.transform.SetSiblingIndex(index2);
        cube2.transform.SetSiblingIndex(index1);

        // Testing swapping the names
        //string tempName = cube1.name;
        //cube1.name = cube2.name;
        //cube2.name = tempName;
    }
    public void StartSteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        if (!isSorting)
        {
            isSorting = true;
            sortingCoroutine = StartCoroutine(SteppedBubbleSortCoroutine(list, onComplete));
        }
    }
    IEnumerator SteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        //getting the list
        List<int> sortedList = new List<int>(list);
        List<int> prevPassList = new List<int>();

        int size = sortedList.Count;
        bool swapped;

        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = new List<GameObject>();
        cubeObjects = cubeGenerator.grabCubes();
        questionLabel.text = "";
        questionLabel.gameObject.SetActive(true);
        MoveTextAboveHighlightedObject(cubeObjects[0], cubeObjects[1]);

        MovePrismAboveHighlightedObject(cubeObjects[0]);
        float adjustedDelay = delayInSeconds * delaySlider.value;
        do
        {

            swapped = false;
            cubeObjects = cubeGenerator.grabCubes();

            for (int i = 1; i < size; i++)
            {

                cubeObjects = cubeGenerator.grabCubes();
                //MovePrismAboveHighlightedObject(cubeObjects[i]);
                yield return new WaitForSeconds(adjustedDelay);

                if (sortedList[i - 1] > sortedList[i])
                {
                    
                    MoveTextAboveHighlightedObject(cubeObjects[i-1], cubeObjects[i]);
                    
                    MovePrismAboveHighlightedObject(cubeObjects[i-1]);
                    yield return new WaitForSeconds(adjustedDelay);
                   
                    
                    prevPassList = sortedList;
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;
                    
                    MovePrismAboveHighlightedObject(cubeObjects[i]);
                    //yield return new WaitForSeconds(0.5f);

                    
                    SwapCubes(cubeObjects[i - 1], cubeObjects[i]);
                    //cubeGenerator.grabandDestroyCubes();
                    //cubeGenerator.InstantiateCubes(sortedList);
                    
                    cubeObjects = cubeGenerator.grabCubes();

                    
                    string sortedString = string.Join(",", sortedList);
                    sortedOutput.text = sortedString;
                    
                    // Introduce a delay
                    yield return new WaitForSeconds(adjustedDelay);
                    swapped = true;
                    if(i != size -1) {
                        MoveTextAboveHighlightedObject(cubeObjects[i], cubeObjects[i + 1]);
                    }

                    
                    MovePrismAboveHighlightedObject(cubeObjects[i]);
                }
                else
                {
                    cubeObjects = cubeGenerator.grabCubes();
                    MoveTextAboveHighlightedObject(cubeObjects[i - 1], cubeObjects[i]);
                    MovePrismAboveHighlightedObject(cubeObjects[i]);
                    yield return new WaitForSeconds(adjustedDelay);
                    

                }
                cubeObjects = cubeGenerator.grabCubes();
                if (i == size -1)
                {
                    questionLabel.transform.localPosition = new Vector3(cubeObjects[i].transform.localPosition.x, questionLabel.transform.localPosition.y + 10 , questionLabel.transform.localPosition.z);
                    questionLabel.text = "size reduced";

                    yield return new WaitForSeconds(adjustedDelay);
                }
            }

            
            
            // Reduce the range for the next pass
            size--;

        } while (swapped);


        Debug.Log("Sorting Complete");
      
        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);
    }
    /// <summary>
    /// Unused, was meant for swapping cubes rather than instantiation & destruction
    /// </summary>
    /// <param name="list"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
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
    /// <summary>
    /// The method invoked when in DIY mode
    /// </summary>
    /// <param name="list"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public IEnumerator TeachSteppedBubbleSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        DisableCubeClickOnAllCubes();
        crossImage.gameObject.SetActive(false);
        tickImage.gameObject.SetActive(false);
        Game1 game1 = new Game1();
        List<int> sortedList = new List<int>(list);
        List<int> prevPassList = new List<int>();
        List<GameObject> cubeObjects = new List<GameObject>();
        int size = sortedList.Count;
        bool swapped;
        
        prismObject = transform.Find("Prism").GetComponent<GameObject>();
        
        Game1.attempts = 0;
        cubeObjects = cubeGenerator.grabCubes(); // NullReferenceException
        
        List<int> cubeNames = new List<int>();
        
        
        //MovePrismAboveHighlightedObject(cubeObjects[0]);
        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                prevPassList = sortedList;
                
                if (sortedList[i - 1] > sortedList[i])
                {
                    
                    //MovePrismAboveHighlightedObject(cubeObjects[i - 1]);
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;
                    //MovePrismAboveHighlightedObject(cubeObjects[i-1]);
                    //MovePrismAboveHighlightedObject(cubeObjects[i]);


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
                        tickImage.gameObject.SetActive(true);
                        SpawnImageOverCube(cubeObjects[i], tickImage);
                        output.text = "Correct";

                        //SQL handling
                        game1.increaseBubbleScore();
                        scoreLabel.text = "Points: " + Game1.totalScore;

                        
                        Game1.questionsAnsweredCorrectly += 1;
                        swapped = true;
                    }
                    else
                    {
                        SpawnImageOverCube(cubeObjects[i], crossImage);
                        Debug.Log("The list of the cube names" + cubeNames);
                        Debug.Log("The sorted list from the bubble sort" + sortedList);
                        //TODO: Incorrect Image:  fix incorrect image
                        //SpawnImageOverCube(, crossImage);
                        crossImage.gameObject.SetActive(true);
                        output.text = "Incorrect";
                        Debug.Log("Sorted List: " + sortedList);
                        Debug.Log("PrevPass List: " + prevPassList);
                        sortedList = prevPassList;
                        scoreLabel.text = "Points: " + Game1.totalScore;
                        //TODO: if incorrect, don't actually swap the cubes, just place the incorrect image and keep the list the same


                        //SQL handling
                        game1.increaseAttempts(); // in this game, i should implement for each attempt, you get minus score, unless your score is already 0
                        swapped = false;
                        yield return null; // will restart the loop but havent tested to what extent, check chat for other implementation
                    }
                    game1.increaseSessionQsAnswered();
                    //swapped = true;

                }
            }



            // Reduce the range for the next pass
            size--;

        } while (swapped && size > 1);

        //SQL Handling 
        //Game1.attempts += 1;
        game1.increaseAttempts();
        
        //game1.scoreArray.Add(game1.tempScore);
        //Debug.Log("Score Array size: " + game1.scoreArray.Count);
        //foreach (int num in game1.scoreArray)
        //{
        //    Debug.Log("Score array " + num);
        //}



        Debug.Log("Sorting Complete");

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);

    }

    private void SpawnImageOverCube(GameObject cube, Image chosenImage)
    {

        // get the main camera
        Camera mainCamera = Camera.main;

        // check if the main camera exists
        if (mainCamera != null)
        {
            // calculate the screen space position of the cube
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(cube.transform.position);

            // Convert the screen space position to canvas space
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, mainCamera, out canvasPosition);

            canvasPosition.y += 400f;


            // Set the tick image's anchored position on the canvas
            chosenImage.rectTransform.anchoredPosition = canvasPosition;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

    //
    private bool ListsAreEqual(List<int> list1, List<int> list2)
    {
        // Check if two lists are equal
        return list1.SequenceEqual(list2);
    }

    /// <summary>
    /// Part of the DIY mode, waiting for the user to drag cubes
    /// </summary>
    /// <param name="cubeObjects"></param>
    /// <returns></returns>
    private IEnumerator WaitForUserSwap(List<GameObject> cubeObjects)
    {
        bool userSwapped = false;
        // Get the initial sibling indexes of the cubes
        List<int> initialSiblingIndexes = cubeObjects.Select(cube => cube.transform.GetSiblingIndex()).ToList();

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
                    //// If the cube hasn't been dragged, check if its name has changed
                    //if (int.Parse(cubeObjects[i].name) != initialNames[i])
                    //{
                    //    cubeDraggedFlags[i] = true;
                    //}
                    // If the cube hasn't been dragged, check if its sibling index has changed
                    if (cubeObjects[i].transform.GetSiblingIndex() != initialSiblingIndexes[i])
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
    private IEnumerator WaitForUserClick(List<GameObject> cubeObjects)
    {
        bool userClicked = false;

        // Get the initial colors of the cubes
        List<Color> initialColors = new List<Color>();
        foreach (var cube in cubeObjects)
        {
            // Assuming the cubes have a component that changes their color
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                initialColors.Add(renderer.material.color);
            }
        }

        // Continue checking for changes until a cube is clicked
        while (!userClicked)
        {
            // Wait for the next frame to avoid performance issues
            yield return null;

            // Check if any cube's color has changed
            for (int i = 0; i < cubeObjects.Count; i++)
            {
                Renderer renderer = cubeObjects[i].GetComponent<Renderer>();
                if (renderer != null && renderer.material.color != initialColors[i])
                {
                    userClicked = true;
                    break;
                }
            }
        }

        if (userClicked)
        {
            Debug.Log("User has clicked a cube");
        }
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
    public void SortMergeButton()
    {
        MergeSortHandler mSH = new MergeSortHandler();
        CubeGenerator cubeGene = new CubeGenerator();
        intList = cubeGenerator.grabCubeNames();

        StartCoroutine(SteppedMergeSortCoroutine(intList, (sortedList) =>
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
    public void MoveTextAboveHighlightedObject(GameObject highlightedObject, GameObject secondHighlightedObject)
    {
        if(highlightedObject != null && secondHighlightedObject != null)
        {
            float xCoordinate = highlightedObject.transform.localPosition.x;
            questionLabel.text = " is " + highlightedObject.name + " > " + secondHighlightedObject.name + "?";
            questionLabel.transform.localPosition = new Vector3(xCoordinate, secondHighlightedObject.transform.localPosition.y + 400f, secondHighlightedObject.transform.localPosition.z);
        }
        else
        {
            Debug.LogError("Highlighted object or questionLabel is null.");
        }

    }
    public void MoveTextAboveHighlightedObjectSearch(GameObject highlightedObject, int target )
    {
        if (highlightedObject != null)
        {
            float xCoordinate = highlightedObject.transform.localPosition.x;
            questionLabel.text = highlightedObject.name + " > " + target + "?";
            questionLabel.transform.localPosition = new Vector3(xCoordinate + 20, highlightedObject.transform.localPosition.y + 300f, highlightedObject.transform.localPosition.z);
        }
        else
        {
            questionLabel.gameObject.SetActive(false);
            Debug.LogError("Highlighted object or questionLabel is null.");
        }

    }

    public void MovePrismAboveHighlightedObject(GameObject highlightedObject)
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
            Debug.LogError("Highlighted object or prism object is null. ");
        }
    }
    public void teachSortButton()
    {
        //Game1 game1 = new Game1();
        enterButton.interactable = false;

        intList = cubeGenerator.grabCubeNames();

        List<int> cubeNames = cubeGenerator.grabCubeNames();
        StartCoroutine(TeachSteppedBubbleSortCoroutine(cubeNames, (sortedCubeNames)  =>
        {
            
            // Handle the sorted list 
            output.text = "Complete";
            string sortedString = string.Join(",", sortedCubeNames);
            sortedOutput.text = sortedString;

            Debug.Log("Temp score when trying to save: " + Game1.tempScore);
            Game1.scoreArray.Add(Game1.totalScore);
            Debug.Log("Score Array size: " + Game1.scoreArray.Count);
            foreach (int num in Game1.scoreArray)
            {
                Debug.Log("Score array " + num);
            }

            // reset temp score
            Game1.totalScore = 0;

        }));

        enterButton.interactable = true;
    }
    public void GenerateRandomButton()
    {
        Clear(() =>
        {
            // This code will be executed after the clear coroutine finishes
            cubeGenerator.InstantiateRandomCubes(4, 9);

        });
        Game1 game1 = new Game1();

        List<GameObject> cubeObjects = cubeGenerator.grabCubes();

        //MovePrismAboveHighlightedObject(cubeObjects[0]);
        
    }
    public void SearchLinearButton()
    {
        List<GameObject> cubeList = cubeGenerator.grabCubes();
        int target = int.Parse(textInputField.text);
        output.text = "type the number you want to find in the input field";
        
        StartCoroutine(SteppedLinearSearchCoroutine(cubeList, target, (index) =>
        {
            if (index != -1)
            {
                output.text = "Target found at index: " + index;
            }
            else
            {
                output.text = "Target not found";
            }
        }));
        sortedList.Clear();
    }
    public IEnumerator SteppedLinearSearchCoroutine(List<GameObject> CubeObjects, int target, Action<int> onComplete)
    {
        // Loop through each element in the list
        for (int i = 0; i < CubeObjects.Count; i++)
        {
            // Highlight the current cube representing the current element being searched
            MovePrismAboveHighlightedObject(CubeObjects[i]);

            // Introduce a delay (replace with your own delay logic)
            yield return new WaitForSeconds(0.5f);

            // Check if the current element matches the target
            if (int.Parse(CubeObjects[i].name) == target)
            {
                // If found, invoke the callback with the index of the target
                onComplete?.Invoke(i);
                yield break; // Exit the coroutine
            }
        }

        // If target not found, invoke the callback with -1 indicating not found
        onComplete?.Invoke(-1);
    }
    public void SearchBinarySearchButton()
    {
        output.text = "type the number you want to find in the input field";
        List<GameObject> cubes = cubeGenerator.grabCubes();
        int target = int.Parse(textInputField.text);

        StartCoroutine(SteppedBinarySearchCoroutine(cubes, target, (foundIndex) =>
        {
            if (foundIndex != -1)
            {
                output.text = $"Target {target} found at index: {foundIndex}";
            }
            else
            {
                output.text = $"Target {target} not found in the list";
            }
        }));
    }
    public void TeachSearchBinarySearchButton()
    {
        
        List<GameObject> cubes = cubeGenerator.grabCubes();
        GameObject randomCube;
        
            // Get a random index within the range of the list
            int randomIndex = Random.Range(0, cubes.Count);

            
            randomCube = cubes[randomIndex];

            int target = int.Parse(randomCube.name);

        output.text = "Find: " + target;
        
        
        

        StartCoroutine(TeachSteppedBinarySearchCoroutine(cubes, target, (foundIndex) =>
        {
            output.text = "Complete";
            if (foundIndex != -1)
            {
                output.text = $"Target {target} found at index: {foundIndex}";
            }
            else
            {
                output.text = $"Target {target} not found in the list";
            }
            
            Game1.scoreArray.Add(Game1.totalScore);
            Debug.Log("Score Array size: " + Game1.scoreArray.Count);
            foreach (int num in Game1.scoreArray)
            {
                Debug.Log("Score array " + num);
            }

            // reset temp score
            Game1.totalScore = 0;
        }));
    }
    IEnumerator SteppedBinarySearchCoroutine(List<GameObject> cubes, int target, Action<int> onComplete)
    {
        prismObject = transform.Find("Prism").GetComponent<GameObject>();
        
        Game1 g1 = new Game1();
        List<int> list = cubeGenerator.grabCubeNames();
        float adjustedDelay = delayInSeconds * delaySlider.value;



        
        int left = 0;
        int right = list.Count - 1;
        questionLabel.text = "sorting...";
        questionLabel.gameObject.SetActive(true);
        //prismObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(adjustedDelay);

        list = g1.BubbleSort(list);

        cubeGenerator.SetCubeTextAndName(cubes, list);

        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<Renderer>().material.color = Color.white;
        }

        yield return new WaitForSeconds(adjustedDelay);
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Highlight the midpoint cube 
            HighlightCube(cubes[mid]);
            MoveTextAboveHighlightedObjectSearch(cubes[mid], target);


            yield return new WaitForSeconds(adjustedDelay);

            // Remove highlight from the midpoint cube 
            RemoveHighlight(cubes[mid]);

            


            // Check if target found at mid
            if (list[mid]== target)
            {
                onComplete?.Invoke(mid);
                yield break;
            }

            // If greater, search the right half
            if (list[mid] < target)
            {
                // Change the color of cubes before mid to black
                for (int i = 0; i < mid; i++)
                {
                    ChangeCubeColorToBlack(cubes[i]);
                }
                left = mid + 1;
            }
            // If smaller, search the left half
            else
            {
                for (int i = mid + 1; i <= right; i++)
                {
                    ChangeCubeColorToBlack(cubes[i]);
                }
                right = mid - 1;
            }

            yield return new WaitForSeconds(adjustedDelay);
            
        }

        // If no finding the target
        onComplete?.Invoke(-1); 
    }
    IEnumerator TeachSteppedBinarySearchCoroutine(List<GameObject> cubes, int target, Action<int> onComplete)
    {
        EnableCubeClickOnAllCubes();
        crossImage.gameObject.SetActive(false);
        tickImage.gameObject.SetActive(false);
        prismObject = transform.Find("Prism").GetComponent<GameObject>();
       
        Game1 g1 = new Game1();
        List<int> list = cubeGenerator.grabCubeNames();
        float adjustedDelay = delayInSeconds * delaySlider.value;




        int left = 0;
        int right = list.Count - 1;
        questionLabel.text = "sorted la";
        questionLabel.gameObject.SetActive(true);

        //prismObject.SetActive(false);
        list = g1.BubbleSort(list);

        cubeGenerator.SetCubeTextAndName(cubes, list);

        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<Renderer>().material.color = Color.white;
        }

        //wait for a colour to change 


        //when colour is selected, check if it was the correct mid
        

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Highlight the midpoint cube 
            //HighlightCube(cubes[mid]);
            //MoveTextAboveHighlightedObjectSearch(cubes[mid], target);
            Debug.Log("reached before");
            yield return StartCoroutine(WaitForUserClick(cubes));
            Debug.Log("reached after");

            List<GameObject> newCubes = cubeGenerator.grabCubes();

            // Remove highlight from the midpoint cube 
            //RemoveHighlight(cubes[mid]);
            GameObject clickedCube = GetClickedCube(newCubes);

            if(int.Parse(clickedCube.name) == list[mid] && int.Parse(clickedCube.name) == target)
            {
                Game1.totalScore += 1;
                Game1.questionsAnsweredCorrectly += 1;
                Game1.sessionQsAnswered += 1;
                SpawnImageOverCube(clickedCube, tickImage);
                crossImage.gameObject.SetActive(false);
                tickImage.gameObject.SetActive(true);
                onComplete?.Invoke(mid);
                yield break;
            }
            else if (int.Parse(clickedCube.name) == list[mid])
            {
                
                SpawnImageOverCube(clickedCube, tickImage);
                crossImage.gameObject.SetActive(false);
                tickImage.gameObject.SetActive(true);
                Game1.totalScore += 1;
                Game1.questionsAnsweredCorrectly += 1;
                Game1.sessionQsAnswered += 1;
                //ChangeAllCubeColorToWhite(cubes);
                
                // If greater, search the right half
                if (list[mid] < target)
                {
                    // Change the color of cubes before mid to black
                    for (int i = 0; i < mid; i++)
                    {
                        ChangeCubeColorToBlack(cubes[i]);
                    }
                    left = mid + 1;
                }
                // If smaller, search the left half
                else
                {
                    for (int i = mid + 1; i <= right; i++)
                    {
                        ChangeCubeColorToBlack(cubes[i]);
                    }
                    right = mid - 1;
                }
                yield return new WaitForSeconds(adjustedDelay);
                ChangeCubeColorToWhite(clickedCube);
            }
            else
            {
                tickImage.gameObject.SetActive(false);
                crossImage.gameObject.SetActive(true);
                SpawnImageOverCube(clickedCube, crossImage);
                Game1.sessionQsAnswered += 1;
                yield return new WaitForSeconds(adjustedDelay);
                ChangeCubeColorToWhite(clickedCube);
            }



            // Check if target found at mid
            //if (list[mid] == target)
            //{
            //    onComplete?.Invoke(mid);
            //    yield break;
            //}

            

            yield return new WaitForSeconds(adjustedDelay);

        }
        
        DisableCubeClickOnAllCubes();
        // If no finding the target
        onComplete?.Invoke(-1);
    }
    GameObject GetClickedCube(List<GameObject> newCubes)
    {
        foreach (GameObject cube in newCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null && renderer.material.color == Color.yellow)
            {
                return cube;
            }
        }
        return null;
    }
    void EnableCubeClickOnAllCubes()
    {
        CubeClick[] cubeClicks = CubeGenTransform.GetComponentsInChildren<CubeClick>();
        foreach (var cubeClick in cubeClicks)
        {
            cubeClick.enabled = true;
        }
    }
    void DisableCubeClickOnAllCubes()
    {
        CubeClick[] cubeClicks = CubeGenTransform.GetComponentsInChildren<CubeClick>();
        foreach (var cubeClick in cubeClicks)
        {
            cubeClick.enabled = false;
        }
    }
    void ChangeAllCubeColorToWhite(List<GameObject> cubes)
    {
        foreach (var cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();

            // Check if the cube has a renderer component
            if (cubeRenderer != null)
            {
                // Change the color of the cube to black
                cubeRenderer.material.color = Color.white;
            }
            else
            {
                Debug.LogError("Cube does not have a Renderer component.");
            }
        }
    }
    void ChangeCubeColorToWhite(GameObject cube)
    {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();

        // Check if the cube has a renderer component
        if (cubeRenderer != null)
        {
            // Change the color of the cube to black
            cubeRenderer.material.color = Color.white;
        }
        else
        {
            Debug.LogError("Cube does not have a Renderer component.");
        }
    }
    void ChangeCubeColorToBlack(GameObject cube)
    {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();

        // Check if the cube has a renderer component
        if (cubeRenderer != null)
        {
            // Change the color of the cube to black
            cubeRenderer.material.color = Color.black;
        }
        else
        {
            Debug.LogError("Cube does not have a Renderer component.");
        }
    }
    void HighlightCube(GameObject cube)
    {
        
        cube.GetComponent<Renderer>().material.color = Color.yellow;
    }

    
    void RemoveHighlight(GameObject cube)
    {
        
        cube.GetComponent<Renderer>().material.color = Color.white; // or whatever the original color was
    }
    public void StartSteppedMergeSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        if (!isSorting)
        {
            isSorting = true;
            sortingCoroutine = StartCoroutine(SteppedMergeSortCoroutine(list, onComplete));
        }
    }
    /*public IEnumerator SteppedMergeSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        
        List<int> sortedList = new List<int>(list);
        List<int> temp = new List<int>(list);

        int size = sortedList.Count;
        float adjustedDelay = delayInSeconds; //* delaySlider.value;

        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = new List<GameObject>();
        cubeObjects = cubeGenerator.grabCubes();
        questionLabel.text = "";
        questionLabel.gameObject.SetActive(true);
        MovePrismAboveHighlightedObject(cubeObjects[0]);

        yield return MergeSortRecursive(sortedList, temp, 0, size - 1, adjustedDelay, cubeObjects);

        Debug.Log("Sorting Complete");

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);
    }*/

    public IEnumerator SteppedMergeSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        List<int> sortedList = new List<int>(list);
        List<GameObject> cubeObjects = new List<GameObject>();
        cubeObjects = cubeGenerator.grabCubes();

        yield return StartCoroutine(MergeSort(sortedList, 0, sortedList.Count - 1, cubeObjects));

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);
    }

    IEnumerator MergeSort(List<int> list, int left, int right, List<GameObject> cubeObjects)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            yield return StartCoroutine(MergeSort(list, left, mid, cubeObjects));
            yield return StartCoroutine(MergeSort(list, mid + 1, right, cubeObjects));
            yield return StartCoroutine(Merge(list, left, mid, right, cubeObjects));
        }
    }

    IEnumerator Merge(List<int> list, int left, int mid, int right, List<GameObject> cubeObjects)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];
        GameObject[] LCubes = new GameObject[n1];
        GameObject[] RCubes = new GameObject[n2];

        for (int p = 0; p < n1; ++p)
        {
            L[p] = list[left + p];
            LCubes[p] = cubeObjects[left + p];
        }
        for (int q = 0; q < n2; ++q)
        {
            R[q] = list[mid + 1 + q];
            RCubes[q] = cubeObjects[mid + 1 + q];
        }

        int k = left;
        int i = 0, j = 0;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                list[k] = L[i];
                cubeObjects[k] = LCubes[i];
                i++;
            }
            else
            {
                list[k] = R[j];
                cubeObjects[k] = RCubes[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            list[k] = L[i];
            cubeObjects[k] = LCubes[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            list[k] = R[j];
            cubeObjects[k] = RCubes[j];
            j++;
            k++;
        }

        // Move cubes accordingly
        MoveCubesAccordingly(cubeObjects, list);

        // Introduce a delay (replace with your own delay logic)
        yield return new WaitForSeconds(0.5f);
    }
    void MoveCubesAccordingly(List<GameObject> cubes, List<int> sortedList)
    {
        // Iterate over the cubes and update their names and labels according to the sorted list
        for (int i = 0; i < cubes.Count; i++)
        {
            GameObject cube = cubes[i];
            int value = sortedList[i];

            // Update cube's name and label
            cube.name = value.ToString();
            TextMeshProUGUI label = cube.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.text = value.ToString();
            }
            else
            {
                Debug.LogError("Label not found on cube: " + cube.name);
            }
        }
    }

    bool IsSorted(List<int> list)
    {
        // Check if the list is sorted in ascending order
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (list[i] > list[i + 1])
            {
                return false;
            }
        }
        return true;
    }
    public void ReturnToPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");

    }
}
