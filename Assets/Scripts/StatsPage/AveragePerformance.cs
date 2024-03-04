using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AveragePerformance : MonoBehaviour
{
    public TextMeshProUGUI averagePointsText;
    public TextMeshProUGUI averageSuccessRateText;
    public TextMeshProUGUI averageQuestionsAnsweredText;
    public Image background;
    

    // Method to calculate average performance metrics
    public void CalculateAverages()
    {
        // Assuming you have collected data for each game session in lists or arrays
        

        // Calculate average points per session
        float averagePoints = CalculateAveragePointsPerSession();


        // Calculate average success rate

        float averageSuccessRate = CalculateAverageSuccessRate();
        StatImageDisplay SID = new StatImageDisplay();
        StatImageDisplay.UpdateSuccessRate(averageSuccessRate, averageSuccessRateText, background);


        // Calculate average number of questions answered per session
        float averageQuestionsAnswered = CalculateAverageQuestionsAnswered();

        // Update UI with calculated averages
        averagePointsText.text = "Average Points per Session: " + averagePoints.ToString();
        averageSuccessRateText.text = "Average Success Rate: " + averageSuccessRate.ToString("P");
        averageQuestionsAnsweredText.text = "Average Questions Answered per Session: " + averageQuestionsAnswered.ToString();
    }

    // Method to calculate average points per session
    private float CalculateAveragePointsPerSession()
    {
        // Initialize variables to store total points and total sessions
        int totalPoints = 0;
        int totalSessions = 0;

        // game1
        foreach (int points in DataHandler.game1PointsPerSession)
        {
            totalPoints += points;
            totalSessions++;
        }

        //game2
        foreach (int points in DataHandler.game2PointsPerSession)
        {
            totalPoints += points;
            totalSessions++;
        }

        //game3
        foreach (int points in DataHandler.game3PointsPerSession)
        {
            totalPoints += points;
            totalSessions++;
        }

        // Calculate average points per session
        float averagePoints = totalSessions > 0 ? (float)totalPoints / totalSessions : 0f;

        return averagePoints;
    }

    // Method to calculate average success rate
    private float CalculateAverageSuccessRate()
    {
        float totalSuccessRate = 0.00f;
        int totalSessions = 0;

        foreach (float rate in DataHandler.game1SessionSuccessRate)
        {
            totalSuccessRate += rate; // Add the percentage directly
            totalSessions++;
        }

        foreach (float rate in DataHandler.game2SessionSuccessRate)
        {
            totalSuccessRate += rate; // Add the percentage directly
            totalSessions++;
        }

        foreach (float rate in DataHandler.game3SessionSuccessRate)
        {
            totalSuccessRate += rate; // Add the percentage directly
            totalSessions++;
        }

        // Calculate average success rate
        float averageSuccessRate = totalSessions > 0 ? totalSuccessRate / totalSessions : 0f;
        averageSuccessRate = averageSuccessRate / 100;

        return averageSuccessRate;
    }

    // Method to calculate average number of questions answered per session
    private float CalculateAverageQuestionsAnswered()
    {
        int totalQuestionsAnswered = 0;
        int totalSessions = 0;

        foreach (int count in DataHandler.game1SessionQsAnswered)
        {
            totalQuestionsAnswered += count;
            totalSessions++;
        }

        foreach (int count in DataHandler.game2SessionQsAnswered)
        {
            totalQuestionsAnswered += count;
            totalSessions++;
        }

        foreach (int count in DataHandler.game3SessionQsAnswered)
        {
            totalQuestionsAnswered += count;
            totalSessions++;
        }

        float averageQuestionsAnswered = totalSessions > 0 ? (float)totalQuestionsAnswered / totalSessions : 0f;

        return averageQuestionsAnswered;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Calculate and display average performance metrics when the scene starts
        CalculateAverages();
    }
}
