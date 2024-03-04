using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    
    public TextMeshProUGUI userStatus;
    public GameObject loadingIcon;
    public void Start()
    {
        if (DBManager.LoggedIn)
        {
            userStatus.text = DBManager.username;
        }
        loadingIcon.SetActive(false);
    }
    public void ReturnToPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");

    }
    public void ReturnToMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
    public void Game1()
    {
        StartCoroutine(LoadGame1());
    }
    public void Game2()
    {
        StartCoroutine(LoadGame2());
        //SQL handling - Resetting sessionQs answered each time user launches Game1 more than once in an open program.
        // i.e 1 session is however long theyre in each respective game.
        
    }
    IEnumerator LoadGame1()
    {
        //SQL handling - Resetting sessionQs answered each time user launches Game1 more than once in an open program.
        // i.e 1 session is however long theyre in each respective game.
        // Activate the loading icon
        loadingIcon.SetActive(true);
        //loadingIcon.gameObject.transform.position = new Vector3(-444, 310, 0);
        loadingIcon.gameObject.transform.localPosition = new Vector3(-444, 310, 0);
        Game1 game1 = new Game1();
        //Game1.sessionQsAnswered = 0;
        game1.resetScores();

        SceneManager.LoadScene("Game1_SortingSearchingScene");

        yield return new WaitForSeconds(1);
        loadingIcon.SetActive(false);
    }
    IEnumerator LoadGame2()
    {
        // Activate the loading icon
        //loadingIcon.gameObject.transform.position = new Vector3(180, 310, 0);
        loadingIcon.gameObject.transform.localPosition = new Vector3(180, 310, 0);
        loadingIcon.SetActive(true);
        G2Script game2 = new G2Script();
        //Game1.sessionQsAnswered = 0;
        game2.resetScores();

        SceneManager.LoadScene("Game2_Algorithms");
        
        yield return new WaitForSeconds(1); 
       
        // Deactivate the loading icon when loading is complete
        loadingIcon.SetActive(false);
    }
}
