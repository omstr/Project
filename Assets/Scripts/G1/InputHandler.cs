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
    public void ButtonInputted()
    {
        intList = this.handleInput();
        G1 g1 = new G1();
        sortedList = g1.BubbleSort(intList);

        output.text = inputString;
        
        string sortedString = string.Join(",", sortedList);
        sortedOutput.text = sortedString;
        //List<GameObject> cubeInstances = cubeGenerator.InstantiateCubes(intList);
        cubeGenerator.InstantiateCubes(intList);
    }
    public void SortButton()
    {
        intList = this.handleInput();

        //Game1 is a child of the Canvas
        Transform game1Transform = transform.parent.Find("Game1");


        if (game1Transform != null)
        {
            // CubeGen is a child of Game1
            Transform cubeGenTransform = game1Transform.Find("CubeGen");

            if (cubeGenTransform != null)
            {
                List<GameObject> cubeObjects = new List<GameObject>();

                // Iterate through children of CubeGen
                foreach (Transform child in cubeGenTransform)
                {
                    // Check if the child is a cube (might need a more specific check)
                    if (child.CompareTag("CubeTag"))  // Assign a tag to your cubes or use a different criterion
                    {
                        cubeObjects.Add(child.gameObject);
                    }
                }

                // Now, cubeObjects contains all the cube GameObjects under "CubeGen"
                StartCoroutine(SortedCoroutine(cubeObjects));
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
    IEnumerator SortedCoroutine(List<GameObject> cubeObjects)
    {
        G1 g1 = new G1();

        List<int> inputList = new List<int>();
        List<int> steppedSortedList = new List<int>();

        foreach (GameObject cube in cubeObjects)
        {
            // Assuming each cube has a child with TextMeshProUGUI component containing the label
            TextMeshProUGUI label = cube.GetComponentInChildren<TextMeshProUGUI>();

            if (label != null && int.TryParse(label.text, out int intValue))
            {
                inputList.Add(intValue);
                Debug.Log("IntValue in the cubeObject: " + intValue);
            }
            else
            {
                Debug.LogError("Label not found or could not parse value.");
            }
            steppedSortedList = g1.SteppedBubbleSort(inputList);
            // Update UI or perform other operations as needed
            string sortString = string.Join(",", steppedSortedList);
            sortedOutput.text = sortString;

            // Introduce a delay of 1 second
            yield return new WaitForSeconds(2f);
        }

        // Sorting is complete, handle the final results
        output.text = string.Join(",", inputList);
        string sortedString = string.Join(",", g1.SteppedBubbleSort(inputList));
        sortedOutput.text = sortedString;

        cubeGenerator.InstantiateCubes(inputList);
    }
}
