using System.Collections;
using System.Collections.Generic;
using TMPro;
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
   
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {

        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text);
        
        ///TODO: Replace WWW with unitywebrequest when it all works
        ///

        //Request object
        WWW www = new WWW("http://localhost/unityprojdb/register.php", form);
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
            Debug.Log("Reg failed. Error code #" + www.text);
        }
    }
    public void VerifyInputs()
    {
        enterButton.interactable = (usernameInput.text.Length >= 4);
    }
}
