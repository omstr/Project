using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class RequestData : MonoBehaviour
{
    public List<Dictionary<string, string>> game1Data;
    public List<Dictionary<string, string>> game2Data;
    private string[,] scoresData;
    public void GetGameData()
    {
        StartCoroutine(RequestMethod());
    }
    IEnumerator RequestMethod()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("game1_scores", "game1_scores"); // Add field for game1_scores
        form.AddField("game2_scores", "game2_scores"); // Add field for game2_scores
        ///TODO: Replace WWW with unitywebrequest when it all works
        //Request object

        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityprojdb/login.php", form);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityprojdb/requestdata.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error connecting to server: " + www.error);
                yield break;
            }

            string response = www.downloadHandler.text;
            Debug.Log("response: " + response);
            //string[] rows = response.Split('\n');
            string[] rows = response.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
            scoresData = new string[rows.Length, 6]; // Assuming 6 columns
            foreach(string str in rows)
            {
                Debug.Log(str);
            }
            
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(','); // Assuming columns are separated by commas
                for (int j = 0; j < columns.Length; j++)
                {
                    scoresData[i, j] = columns[j];
                }
            }

            // Now you can access scoresData array to get the scores
            Debug.Log("Scores Count: " + rows.Length);


            //if (data != null && data.ContainsKey(tableName))
            //{
            //    List<Dictionary<string, string>> tableData = data[tableName];
            //    foreach (Dictionary<string, string> rowData in tableData)
            //    {
            //        // Access individual rows and extract data as needed
            //        string pointsPerSession = rowData["pointsPerSession"];
            //        string sessionQsAnswered = rowData["sessionQsAnswered"];
            //        string sessionSuccessRate = rowData["sessionSuccessRate"];
            //        string attempts = rowData["attempts"];
            //        string timestamp = rowData["timestamp"];

            //        // Store the retrieved data in DBManager or use it as needed
            //    }
            //}
            //else
            //{
            //    EditorUtility.DisplayDialog("Error", "No data found for the user.", "OK");
            //}
        }
    }
}
