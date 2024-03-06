using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AveragePerformance1 : MonoBehaviour
{

    public TextMeshProUGUI averagePointsText;
    public TextMeshProUGUI averageSuccessRateText;
    public TextMeshProUGUI averageQuestionsAnsweredText;
    public Image background;

    // Method to calculate average performance metrics
    public void CalculateG1Averages()
    {



        // Calculate average points per session
        List<float> allAveragePoints = new List<float>();
        allAveragePoints = CalculateAverageG123PointsPerSession();
        float averagePoints = allAveragePoints[0];


        // Calculate average success rate

        List<float> allAverageSuccessRate = new List<float>();
        allAverageSuccessRate = CalculateAverageG123SuccessRate();
        float averageSuccessRate = allAverageSuccessRate[0];

        StatImageDisplay SID = new StatImageDisplay();
        SID.UpdateSuccessRate(averageSuccessRate, averageSuccessRateText, background);


        // Calculate average number of questions answered per session
        List<float> allAverageQAnswered = new List<float>();
        allAverageQAnswered = CalculateAverageG123QuestionsAnswered();
        float averageQuestionsAnswered = allAverageQAnswered[0];

        // Update UI with calculated averages
        averagePointsText.text = "Average Points per Session: " + averagePoints.ToString();
        averageSuccessRateText.text = "Average Success Rate: " + averageSuccessRate.ToString("P");
        averageQuestionsAnsweredText.text = "Average Questions Answered per Session: " + averageQuestionsAnswered.ToString();
    }
    public void CalculateG2Averages()
    {
        


        // Calculate average points per session
        List<float> allAveragePoints = new List<float>();
        allAveragePoints = CalculateAverageG123PointsPerSession();
        float averagePoints = allAveragePoints[1];


        // Calculate average success rate

        List<float> allAverageSuccessRate = new List<float>();
        allAverageSuccessRate = CalculateAverageG123SuccessRate();
        float averageSuccessRate = allAverageSuccessRate[1];
        StatImageDisplay SID = new StatImageDisplay();
        SID.UpdateSuccessRate(averageSuccessRate, averageSuccessRateText, background);


        // Calculate average number of questions answered per session
        List<float> allAverageQAnswered = new List<float>();
        allAverageQAnswered = CalculateAverageG123QuestionsAnswered();
        float averageQuestionsAnswered = allAverageQAnswered[1];

        // Update UI with calculated averages
        averagePointsText.text = "Average Points per Session: " + averagePoints.ToString();
        averageSuccessRateText.text = "Average Success Rate: " + averageSuccessRate.ToString("P");
        averageQuestionsAnsweredText.text = "Average Questions Answered per Session: " + averageQuestionsAnswered.ToString();
    }
    public void CalculateG3Averages()
    {
        


        // Calculate average points per session
        List<float> allAveragePoints = new List<float>();
        allAveragePoints = CalculateAverageG123PointsPerSession();
        float averagePoints = allAveragePoints[2];


        // Calculate average success rate

        List<float> allAverageSuccessRate = new List<float>();
        allAverageSuccessRate = CalculateAverageG123SuccessRate();
        float averageSuccessRate = allAverageSuccessRate[2];

        StatImageDisplay SID = new StatImageDisplay();
        SID.UpdateSuccessRate(averageSuccessRate, averageSuccessRateText, background);


        // Calculate average number of questions answered per session
        List<float> allAverageQAnswered = new List<float>();
        allAverageQAnswered = CalculateAverageG123QuestionsAnswered();
        float averageQuestionsAnswered = allAverageQAnswered[2];

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
    private List<float> CalculateAverageG123PointsPerSession()
    {
        List<float> allAveragePoints = new List<float>();
        // Initialize variables to store total points and total sessions
        int totalPoints1 = 0;
        int totalSessions1 = 0;
        int totalPoints2 = 0;
        int totalSessions2 = 0;
        int totalPoints3 = 0;
        int totalSessions3 = 0;

        // game1
        foreach (int points in DataHandler.game1PointsPerSession)
        {
            totalPoints1 += points;
            totalSessions1++;
        }

        //game2
        foreach (int points in DataHandler.game2PointsPerSession)
        {
            totalPoints2 += points;
            totalSessions2++;
        }

        //game3
        foreach (int points in DataHandler.game3PointsPerSession)
        {
            totalPoints3 += points;
            totalSessions3++;
        }

        // Calculate average points per session
        float averagePoints1 = totalSessions1 > 0 ? (float)totalPoints1 / totalSessions1 : 0f;
        float averagePoints2 = totalSessions2 > 0 ? (float)totalPoints2 / totalSessions2 : 0f;
        float averagePoints3 = totalSessions3 > 0 ? (float)totalPoints3 / totalSessions3 : 0f;

        allAveragePoints.Add(averagePoints1);
        allAveragePoints.Add(averagePoints2);
        allAveragePoints.Add(averagePoints3);
        return allAveragePoints;
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
    private List<float> CalculateAverageG123SuccessRate()
    {
        List<float> allAverageSuccessRates = new List<float>();
        float totalSuccessRate1 = 0.00f;
        int totalSessions1 = 0;
        float totalSuccessRate2 = 0.00f;
        int totalSessions2 = 0;
        float totalSuccessRate3= 0.00f;
        int totalSessions3 = 0;

        foreach (float rate in DataHandler.game1SessionSuccessRate)
        {
            totalSuccessRate1 += rate; // Add the percentage directly
            totalSessions1++;
        }

        foreach (float rate in DataHandler.game2SessionSuccessRate)
        {
            totalSuccessRate2 += rate; // Add the percentage directly
            totalSessions2++;
        }

        foreach (float rate in DataHandler.game3SessionSuccessRate)
        {
            totalSuccessRate3 += rate; // Add the percentage directly
            totalSessions3++;
        }

        // Calculate average success rate
        float averageSuccessRate1 = totalSessions1 > 0 ? totalSuccessRate1 / totalSessions1 : 0f;
        averageSuccessRate1 = averageSuccessRate1 / 100;

        float averageSuccessRate2 = totalSessions2 > 0 ? totalSuccessRate2 / totalSessions2 : 0f;
        averageSuccessRate2 = averageSuccessRate2 / 100;

        float averageSuccessRate3 = totalSessions3 > 0 ? totalSuccessRate3 / totalSessions3 : 0f;
        averageSuccessRate3 = averageSuccessRate3 / 100;

        allAverageSuccessRates.Add(averageSuccessRate1);
        allAverageSuccessRates.Add(averageSuccessRate2);
        allAverageSuccessRates.Add(averageSuccessRate3);
        return allAverageSuccessRates;
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
    private List<float> CalculateAverageG123QuestionsAnswered()
    {
        List<float> allAverageQuestionsAnswered = new List<float>();
        int totalQuestionsAnswered1 = 0;
        int totalSessions1 = 0;
        int totalQuestionsAnswered2 = 0;
        int totalSessions2 = 0;
        int totalQuestionsAnswered3 = 0;
        int totalSessions3 = 0;

        foreach (int count in DataHandler.game1SessionQsAnswered)
        {
            totalQuestionsAnswered1 += count;
            totalSessions1++;
        }

        foreach (int count in DataHandler.game2SessionQsAnswered)
        {
            totalQuestionsAnswered2 += count;
            totalSessions2++;
        }

        foreach (int count in DataHandler.game3SessionQsAnswered)
        {
            totalQuestionsAnswered3 += count;
            totalSessions3++;
        }

        float averageQuestionsAnswered1 = totalSessions1 > 0 ? (float)totalQuestionsAnswered1 / totalSessions1 : 0f;
        float averageQuestionsAnswered2 = totalSessions2 > 0 ? (float)totalQuestionsAnswered2 / totalSessions2 : 0f;
        float averageQuestionsAnswered3 = totalSessions3 > 0 ? (float)totalQuestionsAnswered3 / totalSessions3 : 0f;

        allAverageQuestionsAnswered.Add(averageQuestionsAnswered1);
        allAverageQuestionsAnswered.Add(averageQuestionsAnswered2);
        allAverageQuestionsAnswered.Add(averageQuestionsAnswered3);

        return allAverageQuestionsAnswered;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Calculate and display average performance metrics when the scene starts
        
        CalculateG1Averages();
    
    }
}
