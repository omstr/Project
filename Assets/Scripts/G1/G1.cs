using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class G1 : MonoBehaviour
{
    public CubeGenerator cubeGenerator;
    public TextMeshProUGUI scoreDisplay;
    public int tempScore = 0;
    public int highestScore;
    public int initialScore;
    public int sessionQsAnswered;
    public int attempts;
    public string timestamp;
    List<int> scoreArray = new List<int>();
    //public G1 g1

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
        scoreDisplay.text = "Points: " + DBManager.initialScore;
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        if (cubeGenerator == null)
        {
            Debug.LogError("CubeGenerator not found in the scene.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        if (cubeGenerator == null)
        {
            Debug.LogError("CubeGenerator not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallSaveDataAndReturnToMenu()
    {
        StartCoroutine(CallSaveData());
    }
    public IEnumerator CallSaveData()
    {
        yield return StartCoroutine(SaveUserData());

        InputHandler ihandler = new InputHandler();
        ihandler.ReturnToPlayMenu();


    }
    IEnumerator SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username); //potentially a problem
        //form.AddField("userid", DBManager.userid);
        form.AddField("highScore", DBManager.highScore);
        form.AddField("initialScore", DBManager.initialScore);
        form.AddField("sessionQsAnswered", DBManager.sessionQsAnswered);
        form.AddField("attempts", DBManager.attempts);
        form.AddField("timestamp", DBManager.timestamp);

        WWW www = new WWW("http://localhost/unityprojdb/savedata.php", form);
        yield return www;

        if(www.text[0] == '0')
        {
            Debug.Log("Data Saved.");

        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        //DBManager.LogOut(); - Call this on the play menu
    }
    public void increaseBubbleScore()
    {
        tempScore += 1;
        scoreDisplay.text = "Points: " + tempScore;
        DBManager.initialScore = tempScore;

        


        // Maybe: have to have a total score variable - This will just be for the user? 
        //have to have a per-game score variable that gets reset on teach completion
        //per-game score variable needs to be added to the array
        //have to first set an initialScore on the first time the "teach button" completes
        //then store all the following scores in that same session in an array
        //highScore is max in array
        //attempts is array count
        //timestamp is time when clicking the back button
    }
    public void calculateEndScores()
    {
        
    }

    public List<int> BubbleSort(List<int> list)
    {
        List<int> sortedList = new List<int>(list); 
        int size = sortedList.Count;
        bool swapped;
        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (sortedList[i - 1] > sortedList[i])
                {
                    // Swap elements
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;

                    swapped = true;
                }
            }

            // After each pass, the largest element will be at the end, so reduce the range
            size--;

        } while (swapped);


        return sortedList;
    }

    // For an absolutely unknown reason this method doesnt want to call methods from any other class and i wasted a day on it maybe i'm being stupid but i just moved it to InputHandler
    public List<int> SteppedBubbleSort(List<int> list)
    {
        List<int> sortedList = new List<int>(list);
        int size = sortedList.Count;
        bool swapped;
        //this.SetCubeGenerator(cubeGenerator);

        do
        {
            swapped = false;

            for (int i = 1; i < size; i++)
            {
                if (sortedList[i - 1] > sortedList[i])
                {
                    int temp = sortedList[i - 1];
                    sortedList[i - 1] = sortedList[i];
                    sortedList[i] = temp;

                    swapped = true;
                }
            }

            // After each pass, destroy the previously instantiated cubes from the enter button and instantiate new ones
            Debug.Log("Before destroying cubes. CubeGenerator: " + cubeGenerator);
            cubeGenerator.grabandDestroyCubes();
            Debug.Log("After destroying cubes.");
            Debug.Log("Before instantiating cubes. CubeGenerator: " + cubeGenerator);
            cubeGenerator.InstantiateCubes(sortedList);
            Debug.Log("After instantiating cubes.");
            string sortedString = string.Join(",", sortedList);
            //this.sortedOutput.text = sortedString;


            // Introduce a delay if needed
            // yield return new WaitForSeconds(delayInSeconds);

            // Reduce the range for the next pass
            size--;

        } while (swapped);

        Debug.Log("Sorting Complete");

        return sortedList;
    }



}
