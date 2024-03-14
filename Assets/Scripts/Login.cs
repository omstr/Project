using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject userMenu;
    public GameObject playMenu;
    public GameObject regMenu;
    public GameObject loginMenu;
    public GameObject mainMenu;
    public TMP_InputField usernameInput;
    public Button enterButton;
    private string registeredUsername;

    private DialogManager dialogManager;
    private void Awake()
    {
        dialogManager = transform.Find("DialogManager").GetComponent<DialogManager>();
    }

    public void CallLogin()
    {
        StartCoroutine(LoginMethod());
    }

    IEnumerator LoginMethod()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameInput.text);
        ///TODO: Replace WWW with unitywebrequest when it all works
        //Request object
        string url = "http://omdomalom.atwebpages.com/unityprojdb/login.php";
        WWW www = new WWW(url, form);
        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityprojdb/login.php", form);
        yield return www;
        if (!string.IsNullOrEmpty(www.text))
        {
            //very ugly solution. When a user doesn't have any scores, the 0 pass code is expected 31 characters into www.text
            // changed from 31 to 32 because expecting a single digit userid field
            if (www.text[0] == '0')
            {
                Debug.Log(www.text);

                DBManager.username = usernameInput.text;
                //Need to store scores in DBManager
                //DBManager.G1Scores = int.Parse(www.text.Split(',')[1]);
                SceneManager.LoadScene("MainMenu");
                
            }
            else
            {
                dialogManager.ShowDialog("User Login failed.Error #" + www.text);
                //EditorUtility.DisplayDialog("Error Occurred", "User Login failed. Error #" + www.text, "OK");
                Debug.Log("User Login failed. Error #" + www.text);
            }

        }
        else
        {
            dialogManager.ShowDialog("SQL Server not found");
            //EditorUtility.DisplayDialog("Error connecting to server","SQL Server not found", "OK");
        }
    }

    public void VerifyInputs()
    {
        enterButton.interactable = (usernameInput.text.Length >= 4);
    }

    public void LoadUserMenu()
    {
        SceneManager.LoadScene("UserScene");
    }
}
