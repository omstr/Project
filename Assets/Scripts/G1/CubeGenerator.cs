using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeGenerator : MonoBehaviour
{
    public InputField textInputField;
    //public List<int> intList = new List<int>();
    public List<int> intList = new List<int>();

    //public InputHandler iHandler = new InputHandler();
    public GameObject referenceCube;

    public void InstantiateCubes()
    {


        intList.AddRange(new[] { 1, 2, 3, 4 });
        int size = intList.Count;
        Debug.Log("Size: " + size);
        
        //make 1 cube for the size of the int list entered by the user
        for (int i = 0; i < size -1; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Instantiate(cube, new Vector3(0f, 0f, 0f), Quaternion.identity);
            cube.transform.localScale = new Vector3(100, 400, 40);
        }
    }
  
  
}
