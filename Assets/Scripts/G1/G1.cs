using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class G1 : MonoBehaviour
{
    public CubeGenerator cubeGenerator;
    //public G1 g1

    private void Awake()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        if (cubeGenerator == null)
        {
            Debug.LogError("CubeGenerator not found in the scene.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        if (cubeGenerator == null)
        {
            Debug.LogError("CubeGenerator not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

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

    // For an absolutely unknown reason this method doesnt want to call methods from any other class and i wasted a day on it maybe i'm being stupid but i just moved it to InputHandler
    public List<int> SteppedBubbleSort(List<int> list)
    {
        List<int> sortedList = new List<int>(list);
        int size = sortedList.Count;
        bool swapped;
        //this.SetCubeGenerator(cubeGenerator);

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

                    swapped = true;
                }
            }

            // After each pass, destroy the previously instantiated cubes from the enter button and instantiate new ones
            Debug.Log("Before destroying cubes. CubeGenerator: " + cubeGenerator);
            cubeGenerator.grabandDestroyCubes();
            Debug.Log("After destroying cubes.");
            Debug.Log("Before instantiating cubes. CubeGenerator: " + cubeGenerator);
            cubeGenerator.InstantiateCubes(sortedList);
            Debug.Log("After instantiating cubes.");
            string sortedString = string.Join(",", sortedList);
            //this.sortedOutput.text = sortedString;


            // Introduce a delay if needed
            // yield return new WaitForSeconds(delayInSeconds);

            // Reduce the range for the next pass
            size--;

        } while (swapped);

        Debug.Log("Sorting Complete");

        return sortedList;
    }



}
