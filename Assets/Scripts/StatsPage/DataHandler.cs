using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private RequestData rD; // Reference to the RequestData class
    public TextMeshProUGUI bestGameLabel;
    public List<string[]> game1ScoresData;
    public List<string[]> game2ScoresData;

    // Separate variables to store data for each field
    private List<int> game1PointsPerSession = new List<int>();
    private List<int> game1SessionQsAnswered = new List<int>();
    private List<float> game1SessionSuccessRate = new List<float>();
    private List<int> game1Attempts = new List<int>();
    private List<string> game1Timestamps = new List<string>();

    private List<int> game2PointsPerSession = new List<int>();
    private List<int> game2SessionQsAnswered = new List<int>();
    private List<float> game2SessionSuccessRate = new List<float>();
    private List<int> game2Attempts = new List<int>();
    private List<string> game2Timestamps = new List<string>();


    private List<float> game1SessionSuccessRates = new List<float>();
    private List<float> game2SessionSuccessRates = new List<float>();
    private List<float> game3SessionSuccessRates = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        rD = GetComponent<RequestData>();
        if (rD== null)
        {
            Debug.LogError("RequestData component not found!");
        }

        
    }
    public void StartRequestDataProcess()
    {
        StartCoroutine(FetchData());
    }
    private IEnumerator FetchData()
    {
        // Wait for the RequestMethod coroutine to finish
        yield return StartCoroutine(rD.RequestMethod());

        // Once the RequestMethod coroutine finishes, access the data
        List<string[]> game1Data = rD.getGame1ScoresData();
        List<string[]> game2Data = rD.getGame2ScoresData();

        // Process the retrieved data as needed
        if (game1Data != null && game2Data != null)
        {
            ParseGameData(game1Data, game2Data);
        }
        else
        {
            Debug.LogError("Data Fetched is null or something");
        }
    }
    public void ParseGameData(List<string[]> game1ScoresData, List<string[]> game2ScoresData)
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
            }
        }
    }
    /// <summary>
    /// Used for Best Game stat
    /// </summary>
    /// <param name="successRates"></param>
    /// <returns></returns>
    private float CalculateAverageSuccessRate(List<float> successRates)
    {
        if (successRates.Count == 0)
        {
            return 0f; // Return 0 if there are no success rates
        }

        float sum = 0f;
        foreach (float rate in successRates)
        {
            sum += rate;
        }

        return sum / successRates.Count; // Calculate average
    }
    public void BestGame()
    {
        
        float averageSuccessRateGame1 = CalculateAverageSuccessRate(game1SessionSuccessRates);
        float averageSuccessRateGame2 = CalculateAverageSuccessRate(game2SessionSuccessRates);
        float averageSuccessRateGame3 = CalculateAverageSuccessRate(game3SessionSuccessRates);

        // compare average session success rates and determine the best game
        if (averageSuccessRateGame1 > averageSuccessRateGame2 && averageSuccessRateGame1 > averageSuccessRateGame3)
        {
            bestGameLabel.text = "Best Game: Game 1";
        }
        else if (averageSuccessRateGame2 > averageSuccessRateGame1 && averageSuccessRateGame2 > averageSuccessRateGame3)
        {
            bestGameLabel.text = "Best Game: Game 2";
        }
        else if (averageSuccessRateGame3 > averageSuccessRateGame1 && averageSuccessRateGame3 > averageSuccessRateGame2)
        {
            bestGameLabel.text = "Best Game: Game 3";
        }
        else
        {
            bestGameLabel.text = "No single best game found.";
        }
    }
}


