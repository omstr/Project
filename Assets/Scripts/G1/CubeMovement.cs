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
            initialPosition = transform.position;

            // calculate the offset between the clicked point on the cube and the mouse position
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
                    SwapNamesAndLabels(this.gameObject, otherCube);
                }
            }

            
            isDragging = false;

            // reset the position only if dragging was enabled
            transform.position = initialPosition;
        }
    }
   

    private void Update()
    {
        if (isDragging)
        {
            // update the position of the cube while dragging
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
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
}
