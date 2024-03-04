using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestGameStat : MonoBehaviour
{
    public RectTransform HomePage;
    private TextMeshProUGUI bestGameLabel;
    /// <summary>
    /// Used for Best Game stat
    /// </summary>
    /// <param name="successRates"></param>
    /// <returns></returns>
    private void Awake()
    {
        bestGameLabel = HomePage.Find("BestGameLabel").GetComponent<TextMeshProUGUI>();
        BestGame();
    }
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
    //TODO: Needs more Pizzaz
    public void BestGame()
    {

        float averageSuccessRateGame1 = CalculateAverageSuccessRate(DataHandler.game1SessionSuccessRate);
        float averageSuccessRateGame2 = CalculateAverageSuccessRate(DataHandler.game2SessionSuccessRate);
        float averageSuccessRateGame3 = CalculateAverageSuccessRate(DataHandler.game3SessionSuccessRate);

        // compare average session success rates and determine the best game
        if (averageSuccessRateGame1 > averageSuccessRateGame2 && averageSuccessRateGame1 > averageSuccessRateGame3)
        {
            bestGameLabel.text = "Best Topic: Searching & Sorting";
        }
        else if (averageSuccessRateGame2 > averageSuccessRateGame1 && averageSuccessRateGame2 > averageSuccessRateGame3)
        {
            bestGameLabel.text = "Best Game: Tracing Algorithms";
        }
        else if (averageSuccessRateGame3 > averageSuccessRateGame1 && averageSuccessRateGame3 > averageSuccessRateGame2)
        {
            bestGameLabel.text = "Best Game: Codebreaker";
        }
        else
        {
            bestGameLabel.text = "No single best game found.";
        }
    }
    private void Start()
    {
        
    }
}
