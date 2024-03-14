using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



// Custom class to represent each record in the JSON array
[Serializable]
public class ScoreRecord
{
    public string tablename;
    public string pointsPerSession;
    public string sessionQsAnswered;
    public string sessionSuccessRate;
    public string attempts;
    public string timestamp;
}
public static class JsonHelper
{
    // utility method to deserialize JSON array into an array of objects
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    // wrapper class to deserialize JSON array
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class RequestData : MonoBehaviour
{
    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    public List<string[]> game1ScoresData = new List<string[]>();
    public List<string[]> game2ScoresData = new List<string[]>();
    public List<string[]> game3ScoresData = new List<string[]>();
    public void GetGameData()
    {
        StartCoroutine(RequestMethod());
    }
    public IEnumerator RequestMethod()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("game1_scores", "game1_scores"); // Add field for game1_scores
        form.AddField("game2_scores", "game2_scores"); // Add field for game2_scores
        form.AddField("game3_scores", "game3_scores"); // Add field for game2_scores
        ///TODO: Replace WWW with unitywebrequest when it all works
        //Request object
        string url = "http://omdomalom.atwebpages.com/unityprojdb/requestdata.php";
        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityprojdb/login.php", form);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error connecting to server: " + www.error);
                yield break;
            }

            string response = www.downloadHandler.text;
            Debug.Log("Response: \n" + response);

            // Split the response by newlines to separate data for each table
            string[] lines = response.Split('\n');

            // Initialize variables to store data for the current table
            List<string[]> currentTableData = null;

            foreach (string line in lines)
            {
                // Check if the line indicates the beginning of data for a new table
                if (line.StartsWith("Table:"))
                {
                    string tableName = line.Substring("Table: ".Length).Trim();

                    // Set currentTableData based on the table name
                    switch (tableName)
                    {
                        case "Game1":
                            currentTableData = game1ScoresData;
                            break;
                        case "Game2":
                            currentTableData = game2ScoresData;
                            break;
                        case "Game3":
                            currentTableData = game3ScoresData;
                            break;
                            // Add more cases for additional tables 
                    }
                }
                else
                {
                    // Parse the line as data for the current table and add it to currentTableData
                    if (currentTableData != null)
                    {
                        string[] rowData = line.Split(',');
                        currentTableData.Add(rowData);
                    }
                }
            }

            // Now you can access game1ScoresData and game2ScoresData lists to get the scores
            Debug.Log("Game 1 Scores Count: " + game1ScoresData.Count);
            Debug.Log("Game 2 Scores Count: " + game2ScoresData.Count);
            Debug.Log("Game 3 Scores Count: " + game3ScoresData.Count);

            SceneManager.LoadScene("StatsMenu");

        }
    }
    public List<string[]> getGame1ScoresData()
    {
        return game1ScoresData;
        
    }
    public List<string[]> getGame2ScoresData()
    {
        return game2ScoresData;

    }
    public List<string[]> getGame3ScoresData()
    {
        return game3ScoresData;

    }
}
