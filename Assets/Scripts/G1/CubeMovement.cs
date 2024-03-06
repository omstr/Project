using TMPro;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private bool canDrag = false;
    private bool isDragging = false;
    private GameObject draggedCube;
    private Vector3 initialPosition;
    private Vector3 offset;

    //need these two otherwise dragging no work
    public void EnableDrag()
    {
        canDrag = true;
    }
    public void DisableDrag()
    {
        canDrag = false;
    }
    private void OnMouseDown()
    {
        if (canDrag)
        {
            // store the initial position when the cube is clicked
            initialPosition = transform.localPosition;

            // calculate the offset between the clicked point on the cube and the mouse position
            offset = transform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //TODO: Incorrect Image: check if this works and check if i can return the dragged cube
            if (Physics.Raycast(ray, out hit))
            {
                GameObject draggedCube = hit.collider.gameObject; // store the reference to the clicked cube
            } 

            isDragging = true;
        }
    }
    
    private void OnMouseUp()
    {
        if (isDragging)
        {
            // check if the cube is over another cube
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject otherCube = hit.collider.gameObject;

                if (otherCube.CompareTag("CubeTag"))
                {
                    // Swap names and labels
                    //SwapNamesAndLabels(this.gameObject, otherCube);

                    SwapCubes(this.gameObject, otherCube);
                    
                }
            }

            
            isDragging = false;

            // reset the position only if dragging was enabled
            //transform.localPosition = initialPosition;
        }
    }
   

    private void Update()
    {
        if (isDragging)
        {
            // update the position of the cube while dragging
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.localPosition = curPosition;
        }
    }

    private void SwapNamesAndLabels(GameObject cube1, GameObject cube2)
    {
        // swap names
        string tempName = cube1.name;
        cube1.name = cube2.name;
        cube2.name = tempName;

        // implement similar logic to swap labels 
        TextMeshProUGUI tempLabel = cube1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI otherLabel = cube2.GetComponentInChildren<TextMeshProUGUI>();

        if (tempLabel != null && otherLabel != null)
        {
            string tempText = tempLabel.text;
            tempLabel.text = otherLabel.text;
            otherLabel.text = tempText;
        }
    }
    private void SwapCubes(GameObject cube1, GameObject cube2)
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
}
