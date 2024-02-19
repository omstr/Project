using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterKeySubmit : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;



    // Start is called before the first frame update
    void Start()
    {
        // Attach the Submit function to the onEndEdit event of the input field
        inputField.onEndEdit.AddListener(SubmitInput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SubmitInput(string input)
    {
        // Check if the input is not empty and the Enter key was pressed
        if (!string.IsNullOrEmpty(input) && Input.GetKeyDown(KeyCode.Return))
        {
            // Simulate a button click
            if (submitButton)
            {
                submitButton.onClick.Invoke();
            }
        }
    }
}
