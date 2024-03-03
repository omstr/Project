using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public int tempScore;
    public static string username;
    public static int userid;
    public static int pointsPerSession;
    public static int sessionQsAnswered;
    public static decimal sessionSuccessRate;
    public static int attempts;
    public static string timestamp;
    public static string game;
    //public static string[][] g1Scores;

    public static bool LoggedIn { get { return username != null; } }
    public static void LogOut()
    {
        username = null;
    }

}
