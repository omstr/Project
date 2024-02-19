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
        mainMenu.SetActive(false);
        userMenu.SetActive(true);
    }

    
}
