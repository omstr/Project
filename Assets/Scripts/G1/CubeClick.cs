using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClick : MonoBehaviour
{
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    private GameObject clickedCube;
    private Renderer cubeRenderer;
    private Vector3 offset;
    private void Start()
    {
        // Get the renderer component attached to the cube object
        cubeRenderer = GetComponent<Renderer>();

        // Store the original color of the cube
        if (cubeRenderer != null)
        {
            originalColor = cubeRenderer.material.color;
        }
    }
   
    private void OnMouseDown()
    {
        // Store the initial position when the cube is clicked
        

        // Calculate the offset between the clicked point on the cube and the mouse position
        offset = transform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Perform a raycast to detect if the mouse click hits the cube
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check if the raycast hits any collider
        if (Physics.Raycast(ray, out hit))
        {
            try
            {
                if (hit.collider.gameObject == gameObject)// Check if the hit object is the cube itself
                {
                    // Set the dragged cube to this cube
                    clickedCube = gameObject;

                    cubeRenderer.material.color = highlightColor;


                }
            }
            catch (System.Exception)
            {
                Debug.Log("if i am hazing any problem check zis");
                
            }
           
        }
    }
    private void OnMouseUp()
    {
        // Restore the original color of the cube
        if (cubeRenderer != null)
        {
            //cubeRenderer.material.color = originalColor;
        }
    }
}
