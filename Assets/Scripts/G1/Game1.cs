using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// Bugs with Game 1 that may carry over 
/// 
/// * FIX CLEAR BUTTON NOT STOPPING SORT COROUTINE, "coroutines not found" 
/// * If I have time to implement consistent colours in Game1, either i grab the colours of cubes before destruction, or i might have to completely change the destroy/reinstantiate method and swap the cubes either physically or by swapping names
/// * Tick & incorrect icon not removed on regeneration, or when the user continues the dragging cube
/// * teaching mode has the prism icon helping too much
/// * Incorrect swapping logic not done yet, if the user is incorrect when swapping the cubes, it should reset the list to how it was before the incorrect move, but all the SQL stuff should still execute, +1 attempts, +sessionQsAnswered ..
/// </summary>
public class Game1 : MonoBehaviour
{
    public CubeGenerator cubeGenerator;
    public TextMeshProUGUI scoreDisplay;
    //public List<int> intList = new List<int>();
    //public List<int> sortedList = new List<int>();
    public static int tempScore;
    public static int pointsPerSession;
    public static int sessionQsAnswered;
    public static decimal sessionSuccessRate;
    public static int attempts;
    public static string timestamp;
    public static int questionsAnsweredCorrectly;

    public static List<int> scoreArray = new List<int>();
    public static int totalScore;

    private DialogManager dialogManager;

    //public G1 g1

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        dialogManager = transform.Find("DialogManager").GetComponent<DialogManager>();
       
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();
        if (cubeGenerator == null)
        {
            Debug.LogError("CubeGenerator not found in the scene.");
        }
        scoreDisplay = transform.Find("scoreLabel").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cubeGenerator = GameObject.FindObjectOfType<CubeGenerator>();

        scoreDisplay.text = "Points: " + tempScore;
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
        if (sessionQsAnswered != 0)
        {
            StartCoroutine(CallSaveData());
        }
        else
        {
            InputHandler iHandler = new InputHandler();
            iHandler.ReturnToPlayMenu();
        }

    }
    //Storing the data from the game in the fields that get sent to SQL table
    public IEnumerator CallSaveData()
    {
        int decimalPlaces = 2;
        DateTime currentUtcDateTime = DateTime.UtcNow;
        string strCurrTime = currentUtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        
        
        for(int i = 1; i < scoreArray.Count; i++)
        {
            totalScore = scoreArray[i-1] + scoreArray[i];
        }
        Debug.Log("total score before saving:" + totalScore);
        DBManager.pointsPerSession = totalScore;
        DBManager.sessionQsAnswered = sessionQsAnswered;
        if (sessionQsAnswered != 0)
        {
            sessionSuccessRate = (decimal)(((float)questionsAnsweredCorrectly / sessionQsAnswered) * 100);
            sessionSuccessRate = Math.Round(sessionSuccessRate, decimalPlaces);
        }
        else
        {
            sessionSuccessRate = 0;
        }
        DBManager.sessionSuccessRate = sessionSuccessRate;
        DBManager.attempts = attempts;
        DBManager.timestamp = strCurrTime;
        DBManager.game = "game1";

        yield return StartCoroutine(SaveUserData());

        InputHandler ihandler = new InputHandler();
        ihandler.ReturnToPlayMenu();


    }
    //Creating data to send to the mySQL table
    IEnumerator SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username); //potentially a problem
        //form.AddField("userid", DBManager.userid);
        form.AddField("pointsPerSession", DBManager.pointsPerSession);
        
        form.AddField("sessionQsAnswered", DBManager.sessionQsAnswered);
        form.AddField("sessionSuccessRate", DBManager.sessionSuccessRate.ToString("0.00"));
        form.AddField("attempts", DBManager.attempts);
        form.AddField("timestamp", DBManager.timestamp);
        form.AddField("game", DBManager.game);
        string url = "http://omdomalom.atwebpages.com/unityprojdb/savedata.php";
        WWW www = new WWW(url, form);
        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Data Saved.");

        }
        else
        {
            dialogManager.ShowDialog("Saving Data failed. Error #" + www.text);
            //EditorUtility.DisplayDialog("Error Occurred", "Saving Data failed. Error #" + www.text, "OK");

            Debug.Log("Save failed. Error #" + www.text);
        }

        //DBManager.LogOut(); - Call this on the play menu
    }
    public void resetScores()
    {
        tempScore = 0;
        pointsPerSession = 0;
        sessionQsAnswered = 0;
        sessionSuccessRate = 0;
        attempts = 0;
        timestamp = "";
        questionsAnsweredCorrectly = 0;

        totalScore = 0;

}
    public void increaseBubbleScore()
    {
        
        totalScore += 1;
        //if (scoreDisplay != null)
        //{
            
        //    scoreDisplay.text = "Points: " + tempScore;
        //}
        //else
        //{
            
        //    Debug.LogError("scoreDisplay is null!");
        //}

        // Maybe: have to have a total score variable - This will just be for the user? 
        //have to have a per-game score variable that gets reset on teach completion
        //per-game score variable needs to be added to the array
        //have to first set an initialScore on the first time the "teach button" completes
        //then store all the following scores in that same session in an array
        //highScore is max in array
        //attempts is array count
        //timestamp is time when clicking the back button
    }
    public void increaseSessionQsAnswered()
    {
        sessionQsAnswered += 1;
    }
    public void increaseAttempts()
    {
        attempts += 1;
    }
    public void setTimestamp(string input)
    {
        timestamp = input;
    }
    public void calculateEndScores()
    {

    }
    public List<int> GenerateRandomNumberList(int minSize, int maxSize)
    {
        int size = UnityEngine.Random.Range(minSize, maxSize + 1); // Generate a random size between minSize and maxSize (inclusive)
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < size; i++)
        {
            randomNumbers.Add(UnityEngine.Random.Range(1, 101)); // Generate a random number between 1 and 100
        }

        return randomNumbers;
    }
    /// <summary>
    /// Normal Bubble Sort, used to calculate the answer of the number list beforehand, to be used for comparisons in the DIY bubble sort mode
    /// </summary>
    /// <param></param>
    /// <returns></returns>
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


        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
    public void Merge(int[] arr, int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        // Create temp arrays
        int[] L = new int[n1];
        int[] R = new int[n2];
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        // Merge the temp arrays

        // Initial indexes of first
        // and second subarrays
        i = 0;
        j = 0;

        // Initial index of merged
        // subarray array
        int k = l;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        // Copy remaining elements
            
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
        }

        // Copy remaining elements
            
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
        }
    }

        
    public void MergeSort(int[] arr, int l, int r)
    {
        if (l < r)
        {

            // Find the middle point
            int m = l + (r - l) / 2;

            // Sort first and second halves
            MergeSort(arr, l, m);
            MergeSort(arr, m + 1, r);

            // Merge the sorted halves
            Merge(arr, l, m, r);
        }
    }

    // A utility function to
    // print array of size n
    static void printArray(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; ++i)
            Console.Write(arr[i] + " ");
        Console.WriteLine();
    }

    
    //public static void Main(String[] args)
    //{
    //    int[] arr = { 12, 11, 13, 5, 6, 7 };
    //    Console.WriteLine("Given array is");
    //    printArray(arr);
    //    //MergeSort(arr, 0, arr.Length - 1);
    //    Console.WriteLine("\nSorted array is");
    //    printArray(arr);
    //}


    // For an absolutely unknown reason this method doesnt want to call methods from any other class and i wasted too long on it maybe i'm being stupid
    // but i just moved it and all the logic that was supposed to be here, to InputHandler

    //OLD
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
