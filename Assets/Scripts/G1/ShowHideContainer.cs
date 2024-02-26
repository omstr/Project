using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowHideContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject container; // Reference to the container holding the hidden buttons
    public Button button1; // Reference to the first hidden button
    public Button button2; // Reference to the second hidden button

    private bool isHovering = false; // Flag to track if the cursor is within the container

    void Start()
    {
        // Hide the container and hidden buttons initially
        container.SetActive(false);
        //button1.gameObject.SetActive(false);
        //button2.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        container.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        // Check if the cursor is outside the container
        if (!IsPointerOverContainer())
        {
            container.SetActive(false);
        }
    }

    // Check if the cursor is over the container or any of its children
    private bool IsPointerOverContainer()
    {
        return isHovering || container.GetComponent<RectTransform>().rect.Contains(Input.mousePosition);
    }
}