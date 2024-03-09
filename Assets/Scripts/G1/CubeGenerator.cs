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
    //public RectTransform cubeGenTransform;
    private bool isDragging = false;
    private Vector3 offset;
    private RectTransform thisTransform;

    private Dictionary<string, Color> cubeColors = new Dictionary<string, Color>();
    private void Awake()
    {
        //cubeGenTransform = transform.Find("CubeGen").GetComponent<RectTransform>();
        thisTransform = transform.GetComponent<RectTransform>();
    }
    private void Start()
    {
        
    }
    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public void InstantiateCubes(List<int> iList)
    {

        int size = iList.Count;
        //Debug.Log("Size: " + size);
        // Clear existing cubes before instantiating new ones
        //foreach (Transform child in transform)
        //{
        //    Destroy(child.gameObject);
        //}
        //TODO - FIXED : Bug, not creating the correct amount of blocks according to the size of the list

        //make 1 cube for the size of the int list entered by the user

        float startingPosX = -750f;
        float startingPosY = -75f;
        float startingPosZ = -5f;
        for (int i = 0; i < size; i++)
        {

            
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newCube.AddComponent<CubeMovement>();
            newCube.AddComponent<CubeClick>();
            newCube.GetComponent<CubeClick>().enabled = false;
            Collider collider = newCube.GetComponent<Collider>();

            // Check if a collider is present
            if (collider != null)
            {
                
                collider.isTrigger = true;
            }
            else
            {
                // Add a collider component to the cube and set it as a trigger
                collider = newCube.AddComponent<BoxCollider>();
                collider.isTrigger = true;
            }
            newCube.transform.parent = transform;
            newCube.tag = "CubeTag";

            //newCube.name = "Cube" + i;

            //doesnt work
            //GameObject newCube = Instantiate(cube, new Vector3(i * 2, 0f, 0f), Quaternion.identity);

            float cubeXPosition = (transform.localPosition.x + startingPosX) + i * 180f;
            float cubeYPosition = transform.localPosition.y + startingPosY; //
            float cubeZPosition = startingPosZ; //
            newCube.transform.localPosition = new Vector3(cubeXPosition, cubeYPosition, cubeZPosition);
            newCube.transform.localScale = new Vector3(100, 300, 100);

            //Label assignment
            GameObject label = Instantiate(labelPrefab, new Vector3(0, 0, -0.8f), Quaternion.identity);
            label.transform.SetParent(newCube.transform, false);

            //label.GetComponent<TextMeshProUGUI>().text = iList[i].ToString(); // Assign the number from intList to the label
            TextMeshProUGUI textMeshPro = label.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.enabled = true;
                textMeshPro.text = iList[i].ToString();
                textMeshPro.fontSize = 1;
                textMeshPro.alignment = TextAlignmentOptions.Center;

                newCube.name = iList[i].ToString();
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the label prefab!");
            }
            // Colour Bullshit
            cubeColors[newCube.name] = GetRandomColor();
            Renderer renderer = newCube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = cubeColors[newCube.name];
            }
            else
            {
                Debug.LogError("Renderer component not found on cube prefab!");
            }
        }
    }
    public void InstantiateRandomCubes(int minSize, int maxSize)
    {
        Game1 g1 = new Game1();
        // random list of numbers within the specified size range
        List<int> randomNumbers = g1.GenerateRandomNumberList(minSize, maxSize);
        int size = randomNumbers.Count;
        
        

        
        float startingPosX = -750f;
        float startingPosY = -75f;
        float startingPosZ = -5f;

        // instantiate cubes with random numbers under the "CubeGen" object
        for (int i = 0; i < size; i++)
        {
            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newCube.AddComponent<CubeMovement>();
            newCube.AddComponent<CubeClick>();
            newCube.GetComponent<CubeClick>().enabled = false;
            Collider collider = newCube.GetComponent<Collider>();

            // Check if a collider is present
            if (collider != null)
            {

                collider.isTrigger = true;
            }
            else
            {
                // Add a collider component to the cube and set it as a trigger
                collider = newCube.AddComponent<BoxCollider>();
                collider.isTrigger = true;
            }
            Renderer renderer = newCube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = GetRandomColor();
            }
            else
            {
                Debug.LogError("Renderer component not found on cube prefab!");
            }

            newCube.transform.parent = transform;

            //newCube.transform.SetParent(cubeGenTransform);
            //newCube.transform.parent = cubeGenTransform;
            newCube.tag = "CubeTag";

            float cubeXPosition = (transform.localPosition.x + startingPosX) + i * 180f;
            float cubeYPosition = transform.localPosition.y + startingPosY; //
            float cubeZPosition = startingPosZ;
            newCube.transform.localPosition = new Vector3(cubeXPosition, cubeYPosition, cubeZPosition);


            //newCube.transform.position = new Vector3(-7 + (i * 2), -1.7f, -0.7f);
            newCube.transform.localScale = new Vector3(100, 300, 100);

            // Set the cube's name to the corresponding random number



            GameObject label = Instantiate(labelPrefab, new Vector3(0f, 0f, -0.8f), Quaternion.identity);
            label.transform.SetParent(newCube.transform, false);

            TextMeshProUGUI textMeshPro = label.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.enabled = true;
                textMeshPro.text = randomNumbers[i].ToString();
                textMeshPro.fontSize = 1;
                
                textMeshPro.alignment = TextAlignmentOptions.Center;

                newCube.name = randomNumbers[i].ToString();
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the label prefab!");
            }
        }
    }

    public void moveCubes(GameObject cube1, GameObject cube2)
    {
        // Swap the names of cube1 and cube2
        string tempName = cube1.name;
        cube1.name = cube2.name;
        cube2.name = tempName;
    }
    public void grabandDestroyCubes()
    {
        Debug.Log("Calling grabandDestroyCubes method");
      
            // Ensure the cubeGenTransform is found under game1Transform
            Transform cubeGenTransform = transform;

            if (cubeGenTransform != null)
            {
                List<GameObject> cubeObjects = new List<GameObject>();

                // Iterate through children of "CubeGen"
                foreach (Transform child in cubeGenTransform)
                {
                    if (child.CompareTag("CubeTag"))
                    {
                        // Colour Bullshit that doesnt work
                        string cubeName = child.gameObject.name;
                        if (cubeColors.ContainsKey(cubeName))
                        {
                            // Update cube color in the dictionary
                            Renderer renderer = child.GetComponent<Renderer>();
                            if (renderer != null)
                            {
                                cubeColors[cubeName] = renderer.material.color;
                            }
                            else
                            {
                                Debug.LogError("Renderer component not found on cube object!");
                            }
                        }
                        // Check if the child has any nested objects
                        // If so, destroy them as well
                        foreach (Transform nestedChild in child)
                        {
                            Destroy(nestedChild.gameObject);
                        }

                        // Destroy the root object
                        Destroy(child.gameObject);
                    }
                }
                //return cubeObjects;


            }
            else
            {
                Debug.LogError("Could not find CubeGen transform under Game1.");
            }
        
        
        
        
        
        //return null;

    }
   
    public List<GameObject> grabCubes()
    {
        
        

        if (thisTransform != null)
        {
            List<GameObject> cubeObjects = new List<GameObject>();

            // Iterate through children of "CubeGen"
            foreach (Transform child in thisTransform)
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
        return null;

    }
    public void UpdateLabels(List<GameObject> cubeObjects)
    {
        foreach (GameObject cubeObject in cubeObjects)
        {
            TextMeshProUGUI label = cubeObject.GetComponentInChildren<TextMeshProUGUI>();

            if (label != null)
            {
                // Parse the cube name to an integer and set the label text
                if (int.TryParse(cubeObject.name, out int cubeName))
                {
                    label.text = cubeName.ToString();
                }
                else
                {
                    Debug.LogError("Failed to parse cube name to int: " + cubeObject.name);
                }
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the cube.");
            }
        }
    }
    public List<int> grabCubeNames()
    {
        

        if (transform != null)
        {
            List<int> cubeNameValues = new List<int>();

            // Iterate through children of "CubeGen"
            foreach (Transform child in transform)
            {
                if (child.CompareTag("CubeTag"))
                {
                    // Parse the cube name to an integer
                    if (int.TryParse(child.gameObject.name, out int cubeValue))
                    {
                        cubeNameValues.Add(cubeValue);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse cube name to int: " + child.gameObject.name);
                    }
                }
            }

            return cubeNameValues;
        }
        else
        {
            Debug.LogError("Could not find CubeGen transform under Game1.");
        }

        return null;

       
    }
    public void SetCubeTextAndName(List<GameObject> cubes, List<int> names)
    {
        if (cubes.Count != names.Count)
        {
            Debug.LogError("Number of cubes does not match number of names.");
            return;
        }

        // set their names and label texts
        for (int i = 0; i < cubes.Count; i++)
        {
            GameObject cube = cubes[i];
            int name = names[i];

            // Set the name of the cube
            cube.name = name.ToString();

            // TextMeshProUGUI component is directly on the child object
            TextMeshProUGUI textMeshPro = cube.GetComponentInChildren<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                
                textMeshPro.text = name.ToString();
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the cube.");
            }
        }
    }

    public List<int> GrabCubeText()
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
    public void DestroyCubes()
    {
        Transform cubeGenTransform = transform.Find("CubeGen");

        if (cubeGenTransform != null)
        {
            // Iterate through children of "CubeGen" and destroy them
            foreach (Transform child in cubeGenTransform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            Debug.LogError("Could not find CubeGen transform.");
        }

    }



    


}
