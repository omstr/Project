using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphPlotter2 : MonoBehaviour
{
    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;
    private RectTransform windowGraph;
    private RectTransform windowGraph1;


    public TextMeshProUGUI textLabelTempX;
    public TextMeshProUGUI textLabelTempY;

    private RectTransform tickGraphX;
    private RectTransform tickGraphY;

    private RectTransform highlightingImage;

    private Color[] highlightColors; // Colors to highlight circles from different tables
    private void Awake()
    {
        windowGraph = transform.Find("WindowGraph").GetComponent<RectTransform>();
        //windowGraph = transform.Find("WindowGraph").GetComponent<RectTransform>();
        graphContainer = windowGraph.Find("GraphContainer").GetComponent<RectTransform>();

        tickGraphX = graphContainer.Find("graphTickImgXObj").GetComponent<RectTransform>();
        tickGraphY = graphContainer.Find("graphTickImgYObj").GetComponent<RectTransform>();

        highlightingImage = graphContainer.Find("HighlightingImage").GetComponent<RectTransform>();

        //textLabelTempX = graphContainer.Find("textLabelX").GetComponent<TextMeshProUGUI>();

        highlightColors = new Color[]
        {
        new Color(255,0,0,0.4f),    // Color for table 1
        new Color(0,200,200,0.4f),   // Color for table 2
        new Color(255,255,0,0.4f),  // Color for table 3
        
        };
  
        //data set
      
  
        DisplayGraphFloat(DataHandler.game2SessionSuccessRate);
      

        // HighlightLastCircles(); calling on profile button click

    }
    public void Game1ProfileClick()
    {
        
        StatsButtonController hm = new StatsButtonController();
        hm.LoadGame1ProfileCor();
        
        //DisplayGraphFloat(DataHandler.game1SessionSuccessRate);
    }
    private GameObject CreateDot(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.transform.localScale = new Vector3(3, 3, 3);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;

    }
    private GameObject CreateExpandedDot(Vector2 anchoredPosition, string tableName)
    {
        GameObject gameObject = new GameObject(tableName + "_circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.transform.localScale = new Vector3(3, 3, 3);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;

    }

    public void DisplayGraphFloat(List<float> valuesList)
    {
        graphContainer = windowGraph.Find("GraphContainer").GetComponent<RectTransform>();
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;
        float xSize = 40f;


        GameObject lastDotGameObj = null;
        for (int i = 0; i < valuesList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valuesList[i] / yMax) * graphHeight;
            //creating a dot for each position in the list
            GameObject dotGameObj = CreateDot(new Vector2(xPosition, yPosition));
            //check if have the game object that was created previously. 
            //i.e dealing with the index 0 case
            if (lastDotGameObj != null)
            {
                DrawLines(lastDotGameObj.GetComponent<RectTransform>().anchoredPosition, dotGameObj.GetComponent<RectTransform>().anchoredPosition);
            }
            lastDotGameObj = dotGameObj;

            //create x seperator label
            TextMeshProUGUI textLabelX = Instantiate(textLabelTempX);
            textLabelX.gameObject.transform.SetParent(graphContainer, false);
            textLabelX.gameObject.SetActive(true);
            RectTransform rectTransform = textLabelX.rectTransform;
            rectTransform.anchoredPosition = new Vector2(xPosition, -20f);

            //dashes
            RectTransform tickX = Instantiate(tickGraphX);
            tickX.gameObject.transform.SetParent(graphContainer, false);
            tickX.gameObject.SetActive(true);
            tickX.anchoredPosition = new Vector2(xPosition, -10f);

            int maxTimestampCount = 20;
            //data set
            // SET THE LABEL TO THE TIMESTAMP STRING from DataHandler.game1Timestamps from each index
            try
            {

                // If the timestamps count is greater than 20, take the last previous 20 timestamps
                if (DataHandler.game2Timestamps.Count > maxTimestampCount)
                {
                    int startIndex = DataHandler.game2Timestamps.Count - maxTimestampCount;
                    textLabelX.text = DataHandler.game2Timestamps[startIndex + i].ToString();
                }
                else
                {
                    textLabelX.text = DataHandler.game2Timestamps[i].ToString();
                }

            }
            catch (System.Exception)
            {
                Debug.LogError("Error setting label text. Index out of range.");
                break; // Exit loop if index out of range
            }


        }
        //create Y seperator labels
        int dividerCount = 10;
        for (int i = 0; i <= dividerCount; i++)
        {
            TextMeshProUGUI textLabelY = Instantiate(textLabelTempY);
            textLabelY.gameObject.transform.SetParent(graphContainer, false);
            textLabelY.gameObject.SetActive(true);
            RectTransform rectTransform = textLabelY.rectTransform;

            float normalisedValue = i * 1f / dividerCount;
            rectTransform.anchoredPosition = new Vector2(-25f, normalisedValue * graphHeight);
            textLabelY.text = Mathf.RoundToInt(normalisedValue * yMax).ToString();


        }
        for (int i = 0; i < dividerCount; i++)
        {
            //dashes
            RectTransform tickY = Instantiate(tickGraphY);
            tickY.gameObject.transform.SetParent(graphContainer, false);
            tickY.gameObject.SetActive(true);
            float normalisedValue = i * 1f / dividerCount;
            tickY.anchoredPosition = new Vector2(-7f, normalisedValue * graphHeight + 45);
        }
    }
    public void HighlightLastCircles()
    {
        Dictionary<string, Transform> lastCircles = new Dictionary<string, Transform>();

        // Iterate through child objects of graphContainer
        foreach (Transform child in graphContainer)
        {
            // Check if the child name contains "_circle"
            if (child.name.Contains("_circle"))
            {
                string tableName = child.name.Split('_')[0]; // Extract table name from child name

                // Update the last circle for the current table
                lastCircles[tableName] = child;
            }
        }

        // Highlight the last circles for each table
        foreach (var kvp in lastCircles)
        {
            Transform lastCircle = kvp.Value;
            string tableName = kvp.Key;

            // Instantiate an image to highlight the circles
            RectTransform highlightImage = Instantiate(highlightingImage);
            highlightImage.SetParent(graphContainer, false);

            // Adjust width of the highlight image to cover the range from the first to the last circle
            float minX = graphContainer.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
            float maxX = lastCircle.GetComponent<RectTransform>().anchoredPosition.x;
            float width = maxX - minX;
            highlightImage.anchoredPosition = new Vector2(minX, 0f);
            highlightImage.sizeDelta = new Vector2(width, graphContainer.sizeDelta.y);

            // Set color of the highlight image based on the table
            int tableIndex = int.Parse(tableName.Substring(tableName.Length - 1)) - 1;
            highlightImage.GetComponent<Image>().color = highlightColors[tableIndex];
        }
    }
    private void DrawLines(Vector2 dotPos, Vector2 dotPos2)
    {
        GameObject gameObj = new GameObject("Line", typeof(Image));
        gameObj.transform.SetParent(graphContainer, false);

        //Start to rotate the dots to draw the completed lines
        gameObj.GetComponent<Image>().color = new Color(1, 1, 1, .5f);

        RectTransform rectTransform = gameObj.GetComponent<RectTransform>();
        Vector2 direction = (dotPos2 - dotPos).normalized;
        float distance = Vector2.Distance(dotPos, dotPos2);

        //setting to lower left
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPos + direction * distance * .5f; // mid point between the two dots

        rectTransform.localEulerAngles = new Vector3(0, 0, Vector2ToAngle(direction));

    }

    public static float Vector2ToAngle(Vector2 direction)
    {
        // Use Mathf.Atan2 to get the angle in radians
        float angleRad = Mathf.Atan2(direction.y, direction.x);

        // Convert radians to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;

        
        if (angleDeg < 0)
        {
            angleDeg += 360f;
        }

        return angleDeg;
    }
}
