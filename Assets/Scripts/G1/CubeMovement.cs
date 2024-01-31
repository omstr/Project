using TMPro;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isDragging)
        {
            CubeMovement otherCube = other.GetComponent<CubeMovement>();
            if (otherCube != null && otherCube != this)
            {
                SwapCubeProperties(otherCube);
                otherCube.SwapCubeProperties(this);
            }
        }
    }
    //void OnMouseOver()
    //{
    //    if (isDragging)
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            CubeMovement otherCube = hit.collider.GetComponent<CubeMovement>();
    //            if (otherCube != null && otherCube != this)
    //            {
    //                SwapCubeProperties(otherCube);
    //                otherCube.SwapCubeProperties(this);
    //            }
    //        }
    //    }
    //}

    private void SwapCubeProperties(CubeMovement otherCube)
    {
        // Add your logic here to swap properties between cubes
        // For example, swapping names or labels
        string tempName = otherCube.gameObject.name;
        otherCube.gameObject.name = gameObject.name;
        gameObject.name = tempName;

        TextMeshProUGUI tempLabel = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI otherLabel = otherCube.GetComponentInChildren<TextMeshProUGUI>();

        if (tempLabel != null && otherLabel != null)
        {
            string tempText = tempLabel.text;
            tempLabel.text = otherLabel.text;
            otherLabel.text = tempText;
        }
    }
}
