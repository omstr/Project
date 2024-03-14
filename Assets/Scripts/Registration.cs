using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public GameObject userMenu;
    public GameObject playMenu;
    public GameObject regMenu;
    public GameObject loginMenu;
    public TMP_InputField usernameInput;
    public Button enterButton;
    private string registeredUsername;

    private DialogManager dialogManager;
    private void Awake()
    {
        dialogManager = transform.Find("DialogManager").GetComponent<DialogManager>();
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        string encodedUsername = WWW.EscapeURL(usernameInput.text);
        WWWForm form = new WWWForm();
        form.AddField("username", encodedUsername);

        ///TODO: Replace WWW with unitywebrequest when it all works
        ///

        //Request object
        string url = "http://omdomalom.atwebpages.com/unityprojdb/register.php";
        WWW www = new WWW(url, form);
        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityprojdb/register.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            //go back to main menu
            Debug.Log("User Regged Successfully");
            regMenu.SetActive(false);
            userMenu.SetActive(true);
        }
        else
        {
            dialogManager.ShowDialog("Registration failed. Error #" + www.text);
            //EditorUtility.DisplayDialog("Error Occurred", "Registration failed. Error #" + www.text, "OK");

            Debug.Log("Reg failed. Error code #" + www.text);
        }
    }
    public void VerifyInputs()
    {
        enterButton.interactable = (usernameInput.text.Length >= 4);
        //if (usernameInput.text.Equals(""))
        //{
        //    EditorUtility.DisplayDialog("Empty Username", "Username Field cannot be empty", "OK");
        //}
    }
}
