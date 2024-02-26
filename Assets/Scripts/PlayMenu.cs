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
        //SQL handling - Resetting sessionQs answered each time user launches Game1 more than once in an open program.
        // i.e 1 session is however long theyre in each respective game.
        Game1 game1 = new Game1();
        //Game1.sessionQsAnswered = 0;
        game1.resetScores();

        SceneManager.LoadScene("Game1_SortingSearchingScene");
    }
    public void Game2()
    {
        //SQL handling - Resetting sessionQs answered each time user launches Game1 more than once in an open program.
        // i.e 1 session is however long theyre in each respective game.
        G2Script game2 = new G2Script();
        //Game1.sessionQsAnswered = 0;
        game2.resetScores();

        SceneManager.LoadScene("Game2_Algorithms");
    }
}
