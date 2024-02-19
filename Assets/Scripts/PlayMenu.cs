using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public TextMeshProUGUI userStatus;
    public void Start()
    {
        if (DBManager.LoggedIn)
        {
            userStatus.text = DBManager.username;
        }
    }
    public void ReturnToPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");

    }
    public void ReturnToMainMenu()
    {
        DBManager.LogOut();
        SceneManager.LoadScene("MenuScene");
    }
    public void Game1()
    {
        SceneManager.LoadScene("Game1_SortingSearchingScene");
    }
}
