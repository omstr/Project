using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Game1()
    {
        SceneManager.LoadScene("Game1_SortingSearchingScene");
    }
}
