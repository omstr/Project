using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class G3Script : MonoBehaviour
{
    public static int pointsPerSession;
    public static int sessionQsAnswered;
    public static decimal sessionSuccessRate;
    public static int attempts;
    public static string timestamp;
    public static int questionsAnsweredCorrectly;

    public static List<int> scoreArray = new List<int>();
    public static int totalScore;
    private void Awake()
    {
        // TODO: Uncomment after testing.
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    // Start is called before the first frame update
    void Start()
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
    public IEnumerator CallSaveData()
    {
        int decimalPlaces = 2;

        DateTime currentUtcDateTime = DateTime.UtcNow;
        string strCurrTime = currentUtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");


        //for (int i = 1; i < scoreArray.Count; i++)
        //{
        //    totalScore = scoreArray[i - 1] + scoreArray[i];
        //}

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
        DBManager.game = "game3";

        yield return StartCoroutine(SaveUserData());

        InputHandler ihandler = new InputHandler();
        ihandler.ReturnToPlayMenu();


    }
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


        WWW www = new WWW("http://localhost/unityprojdb/savedata.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Data Saved.");

        }
        else
        {
            EditorUtility.DisplayDialog("Error Occurred", "Saving Data failed. Error #" + www.text, "OK");
            Debug.Log("Save failed. Error #" + www.text);
        }

        //DBManager.LogOut(); - Call this on the play menu
    }

    public void resetScores()
    {
        totalScore = 0;
        pointsPerSession = 0;
        sessionQsAnswered = 0;
        sessionSuccessRate = 0;
        attempts = 0;
        timestamp = "";
        questionsAnsweredCorrectly = 0;

        totalScore = 0;
    }
}
