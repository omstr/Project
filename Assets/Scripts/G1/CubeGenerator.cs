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

    public void InstantiateCubes(List<int> iList)
    {

        int size = iList.Count;
        Debug.Log("Size: " + size);
        // Clear existing cubes before instantiating new ones
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //TODO - FIXED : Bug, not creating the correct amount of blocks according to the size of the list
        //make 1 cube for the size of the int list entered by the user
        float startingPosX = -7;
        float startingPosY = -2;
        float startingPosZ = -5;
        for (int i = 0; i < size; i++)
        {
            Debug.Log("Setting Scale for cube " + i);
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //doesnt work
            //GameObject newCube = Instantiate(cube, new Vector3(i * 2, 0f, 0f), Quaternion.identity);
            newCube.transform.parent = transform;
            newCube.transform.position = new Vector3(-7 + (i*2), -1.7f, -0.7f);
            newCube.transform.localScale = new Vector3(100, 400, 100);

            //Label assignment
            GameObject label = Instantiate(labelPrefab, newCube.transform.position + new Vector3(0,0, -0.8f), Quaternion.identity);
            label.transform.parent = newCube.transform;
            //label.GetComponent<TextMeshProUGUI>().text = iList[i].ToString(); // Assign the number from intList to the label
            TextMeshProUGUI textMeshPro = label.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = iList[i].ToString();
                textMeshPro.fontSize = 2;
                textMeshPro.alignment = TextAlignmentOptions.Center;

            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the label prefab!");
            }
        }
    }
  
  
}
