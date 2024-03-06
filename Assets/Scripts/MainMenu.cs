using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject userMenu;
    public TextMeshProUGUI userStatus;
    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            userStatus.text = DBManager.username;
        }
        //mainMenu.SetActive(true);
        //userMenu.SetActive(false);
        //playMenu.SetActive(false);
    }
    public void LoadPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    public void LoadUserMenu()
    {
        SceneManager.LoadScene("UserScene");
    }
    public void LoadProfile()
    {
        // Retrieve the existing DataHandler instance in the scene
        DataHandler dH = FindObjectOfType<DataHandler>();

        // Check if a DataHandler instance exists
        if (dH != null)
        {
            // Start the data retrieval process
            //dH.StartRequestDataProcess();
            dH.FetchDataAndExecuteStatsMethods();
        }
        else
        {
            Debug.LogError("DataHandler component not found in the scene.");
        }
        

        
        
    }
    public void Exit()
    {
        DBManager.LogOut();
        SceneManager.LoadScene("UserScene");
    }

}
