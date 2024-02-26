using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public Button myButton;
    public TMP_InputField inputField;

    //Input Field
    private void Start()
    {
        // check if the EventSystem is present
        if (EventSystem.current != null && inputField != null)
        {
            // highlight the InputField
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);

            
            inputField.MoveTextEnd(false);
        }
    }
    //Button
    private void Update()
    {
        // check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // check if the EventSystem is present
            if (EventSystem.current != null)
            {
                // highlight the Button
                EventSystem.current.SetSelectedGameObject(myButton.gameObject);

                // invoke a click on the Button
                myButton.onClick.Invoke();
            }
        }
    }
}
