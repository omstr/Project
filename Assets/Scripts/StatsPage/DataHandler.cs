using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class SuccessRateData
{
    public float successRate;
    public string table;

    public SuccessRateData(float successRate, string table)
    {
        this.successRate = successRate;
        this.table = table;
    }
}
public class TimestampData
{
    public string timestamp;
    public string table;

    public TimestampData(string timestamp, string table)
    {
        this.timestamp = timestamp;
        this.table = table;
    }
}
public class DataHandler : MonoBehaviour
{
   
    
    public List<string[]> game1ScoresData;
    public List<string[]> game2ScoresData;
    public List<string[]> game3ScoresData;

    // Separate variables to store data for each field
    public static List<int> game1PointsPerSession = new List<int>();
    public static List<int> game1SessionQsAnswered = new List<int>();
    public static List<float> game1SessionSuccessRate = new List<float>();
    public static List<int> game1Attempts = new List<int>();
    public static List<string> game1Timestamps = new List<string>();

    public static List<int> game2PointsPerSession = new List<int>();
    public static List<int> game2SessionQsAnswered = new List<int>();
    public static List<float> game2SessionSuccessRate = new List<float>();
    public static List<int> game2Attempts = new List<int>();
    public static List<string> game2Timestamps = new List<string>();

    public static List<int> game3PointsPerSession = new List<int>();
    public static List<int> game3SessionQsAnswered = new List<int>();
    public static List<float> game3SessionSuccessRate = new List<float>();
    public static List<int> game3Attempts = new List<int>();
    public static List<string> game3Timestamps = new List<string>();

    public static List<SuccessRateData> superSuccessionSuccessRate = new List<SuccessRateData>();
    public static List<TimestampData> superTimestamps = new List<TimestampData>();
    private RequestData rD;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }
    public void FetchDataAndExecuteStatsMethods()
    {
        StartCoroutine(StartRequestDataProcess());
        
    }
    private IEnumerator StartRequestDataProcess()
    {
        yield return StartCoroutine(FetchData());
    }
    private IEnumerator FetchData()
    {
        rD = FindObjectOfType<RequestData>();

        // Check if a RequestData instance exists
        if (rD == null)
        {
            Debug.LogError("RequestData component not found in the scene.");
        }

        // Wait for the RequestMethod coroutine to finish
        yield return StartCoroutine(rD.RequestMethod());

        // Once the RequestMethod coroutine finishes, access the data
        List<string[]> game1Data = rD.getGame1ScoresData();
        List<string[]> game2Data = rD.getGame2ScoresData();
        List<string[]> game3Data = rD.getGame3ScoresData();

        // Process the retrieved data as needed
        if (game1Data != null && game2Data != null && game3Data != null)
        {
            ParseGameData(game1Data, game2Data,game3Data);
        }
        else
        {
            Debug.LogError("Data Fetched is null or something");
        }
    }
    public void ParseGameData(List<string[]> game1ScoresData, List<string[]> game2ScoresData, List<string[]> game3ScoresData)
    {
        
        foreach (string[] data in game1ScoresData)
        {
            if (data != null && data.Length >= 5) // Check if data array is not null and has at least 5 elements
            {
                game1PointsPerSession.Add(int.Parse(data[0]));
                game1SessionQsAnswered.Add(int.Parse(data[1]));
                game1SessionSuccessRate.Add(float.Parse(data[2]));
                game1Attempts.Add(int.Parse(data[3]));
                game1Timestamps.Add(data[4]);

                superSuccessionSuccessRate.Add(new SuccessRateData(float.Parse(data[2]), "Game1"));
                superTimestamps.Add(new TimestampData(data[4], "Game1"));
            }
        }

        foreach (string[] data in game2ScoresData)
        {
            if (data != null && data.Length >= 5) // Check if data array is not null and has at least 5 elements
            {
                game2PointsPerSession.Add(int.Parse(data[0]));
                game2SessionQsAnswered.Add(int.Parse(data[1]));
                game2SessionSuccessRate.Add(float.Parse(data[2]));
                game2Attempts.Add(int.Parse(data[3]));
                game2Timestamps.Add(data[4]);

                superSuccessionSuccessRate.Add(new SuccessRateData(float.Parse(data[2]), "Game2"));
                superTimestamps.Add(new TimestampData(data[4], "Game2"));
            }
        }
        foreach (string[] data in game3ScoresData)
        {
            if (data != null && data.Length >= 5) // Check if data array is not null and has at least 5 elements
            {
                game3PointsPerSession.Add(int.Parse(data[0]));
                game3SessionQsAnswered.Add(int.Parse(data[1]));
                game3SessionSuccessRate.Add(float.Parse(data[2]));
                game3Attempts.Add(int.Parse(data[3]));
                game3Timestamps.Add(data[4]);

                superSuccessionSuccessRate.Add(new SuccessRateData(float.Parse(data[2]), "Game3"));
                superTimestamps.Add(new TimestampData(data[4], "Game3"));
            }
        }
    }
   
}


