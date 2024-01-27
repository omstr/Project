using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeGenerator : MonoBehaviour
{
    public InputField textInputField;
    public List<int> intList = new List<int>();
    public GameObject referenceCube;
    public GameObject labelPrefab;

    public void InstantiateCubes(List<int> intList)
    {
        List<GameObject> cubeList = new List<GameObject>();
        int size = intList.Count;

        //TODO - FIXED : Bug, not creating the correct amount of blocks according to the size of the list
        // Clear existing cubes before instantiating new ones
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate cubes and assign labels
        // Make 1 Cube for the count of the list entered by the user
        for (int i = 0; i < size; i++)
        {
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //doesnt work
            //GameObject newCube = Instantiate(cube, new Vector3(i * 2, 0f, 0f), Quaternion.identity);
            newCube.tag = "CubeTag";
            newCube.transform.parent = transform;
            newCube.transform.position = new Vector3(-7 + (i * 2), -1.7f, -0.7f);
            newCube.transform.localScale = new Vector3(100, 400, 100);


            // Label assignment
            GameObject label = Instantiate(labelPrefab, newCube.transform.position + new Vector3(0, 0, -0.8f), Quaternion.identity);
            label.transform.parent = newCube.transform;
            TextMeshProUGUI textMeshPro = label.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                textMeshPro.enabled = true;
                textMeshPro.text = intList[i].ToString();
                textMeshPro.fontSize = 1;
                textMeshPro.alignment = TextAlignmentOptions.Center;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the label prefab!");
            }

            // Add the cube to the list
            cubeList.Add(newCube);
        }

        //return cubeList;
    }
    public void swapCubes(List<GameObject> cubeObjects)
    {

    }
  
  
}
