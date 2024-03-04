using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMethods : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //private void OnEnable()
    //{
    //    // Subscribe to the sceneLoaded event
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    // Unsubscribe from the sceneLoaded event to avoid memory leaks
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // Check if the loaded scene is the desired scene
    //    if (scene.name == "StatsMenu")
    //    {
    //        // Run your methods here
    //        //HomeMethods hm = new HomeMethods();
    //        //hm.BestGame();
    //        BestGame();
    //    }
    //}

    public void LoadHomeMethods()
    {
        BestGameStat bGS = new BestGameStat();
        bGS.BestGame();
        AveragePerformance avgPerf = new AveragePerformance();
        avgPerf.CalculateAverages();
    }


}
