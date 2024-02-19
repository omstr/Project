using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static string username;
    public static int userid;
    public static int highScore;
    public static int initialScore;
    public static int sessionQsAnswered;
    public static int attempts;
    public static int timestamp;
    public static string[][] g1Scores;

    public static bool LoggedIn { get { return username != null; } }
    public static void LogOut()
    {
        username = null;
    }

}
