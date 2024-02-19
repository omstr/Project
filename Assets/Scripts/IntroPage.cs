using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroPage : MonoBehaviour
{
    public GameObject introMenu;
    public GameObject playMenu;
    public GameObject regMenu;
    public GameObject loginMenu;
    private string inputString;
    private string registeredUsername;
    // Start is called before the first frame update
    void Start()
    {
        //introMenu.SetActive(true);
        //regMenu.SetActive(false);
        //loginMenu.SetActive(false);
        ////mainMenu.SetActive(false);
        //introMenu.SetActive(false);
        //playMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void loadintroMenu()
    {
        introMenu.SetActive(true);
        regMenu.SetActive(false);
        loginMenu.SetActive(false);
    }
    public void loginButtonClicked()
    {
        //grab Username from SQL and check it exists, if it does then:

        //set playmenu active
        loginMenu.SetActive(false);
        playMenu.SetActive(true);

        //else
        //label.text = "No User Found";
        //label.color = red;


    }
    public void loadRegisterMenu()
    {
        introMenu.SetActive(false);
        regMenu.SetActive(true);
    }
    public void loadLoginMenu()
    {
        introMenu.SetActive(false);
        loginMenu.SetActive(true);
    }
    public void LoadPlayMenu()
    {
        introMenu.SetActive(false);
        playMenu.SetActive(true);
    }
}


