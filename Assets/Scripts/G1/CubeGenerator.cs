using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeGenerator : MonoBehaviour
{
    public InputField textInputField;
    public List<int> intList = new List<int>();
    public GameObject referenceCube;
    //public GameObject labelPrefab;

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
        for (int i = 0; i < size; i++)
        {
            Debug.Log("Setting Scale for cube " + i);
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //doesnt work
            //GameObject newCube = Instantiate(cube, new Vector3(i * 2, 0f, 0f), Quaternion.identity);
            newCube.transform.parent = transform;
            newCube.transform.position = new Vector3(i * 2, 0, 0);
            newCube.transform.localScale = new Vector3(100, 400, 40);

            //Label assignment
            //GameObject label = Instantiate(labelPrefab, newCube.transform.position, Quaternion.identity);
            //label.transform.parent = newCube.transform;
            //label.GetComponent<Text>().text = iList[i].ToString(); // Assign the number from intList to the label
        }
    }
  
  
}
