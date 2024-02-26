using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText; // text component to display the code
    public Button q1Button; // change to button array 
    public Button q2Button; // change to button array 
    public Button q3Button; // change to button array 

    public Button q4Button; // change to button array 
    public Button q5Button; // change to button array 

    public Button[] questionButtons;

    public GameObject codePanel; 
    public TextMeshProUGUI questionLabel;
    public TMP_InputField userInputField;
    public Image buttonImage; 
    public Sprite correctSprite;
    public Sprite orangeSprite;

    

    public int result; // for int comparisons

    public int strResult; // for string comparisons
    
    private string q1ScriptPath = "Assets/Scripts/G2/Q1Fib.cs"; 
    private string q2ScriptPath = "Assets/Scripts/G2/Q2.cs";
    private string q3ScriptPath = "Assets/Scripts/G2/Q3.cs";
    private string q4ScriptPath = "Assets/Scripts/G2/Q4.cs";
    private string q5ScriptPath = "Assets/Scripts/G2/Q5.cs";
    private string q6ScriptPath = "Assets/Scripts/G2/Q6.cs";
    private string q7ScriptPath = "Assets/Scripts/G2/Q7.cs";
    private string q8ScriptPath = "Assets/Scripts/G2/Q8.cs";
    private string q9ScriptPath = "Assets/Scripts/G2/Q9.cs";
    private string q10ScriptPath = "Assets/Scripts/G2/Q10.cs";
    

    void Start()
    {
        buttonImage.gameObject.SetActive(false);
        // Add a listener to the Q1Button click event
        q1Button.onClick.AddListener(ShowQ1Code);
        q2Button.onClick.AddListener(ShowQ2Code);
        q3Button.onClick.AddListener(ShowQ3Code);
        q4Button.onClick.AddListener(ShowQ4Code);
        q5Button.onClick.AddListener(ShowQ5Code);

        userInputField.onEndEdit.AddListener(OnEndEdit);
    }
    void Update()
    {
        

    }
    private void OnEndEdit(string value)
    {
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            
            CheckAnswer();
        }
    }
    public void CheckAnswer()
    {
        // Get the reference to the button that was clicked from other class.
        Button selectedButton = ButtonSelector.selectedButton;
        

        // TODO: ONLY ACCEPTS NUMBERS RIGHT NOW
        // SHOULD PROBABLY START A SWITCH CASE IN A METHOD FOR DIFF TYPES OF Qs
        int userAnswerInt = int.Parse(userInputField.text);
        string userAnswerStr = userInputField.text;


        //string result = GetCorrectAnswerForButton(selectedButton);
        // Compare the user's input with the actual result
        string buttonName = selectedButton.name;
        Transform buttonImageTransform = selectedButton.transform.Find("buttonImage");
        if (buttonImageTransform != null)
        {
            // Get the Image component attached to the child GameObject
            Image storedImage = buttonImageTransform.GetComponent<Image>();
            if (storedImage != null)
            {

                //if (userAnswer == result)
                //{
                //    Debug.Log("Correct answer!");

                //    storedImage.sprite = correctSprite;
                //    storedImage.gameObject.SetActive(true);

                //    G2Script.totalScore += 1;
                //    G2Script.questionsAnsweredCorrectly += 1;


                //}
                //else
                //{
                //    Debug.Log("Incorrect answer!");
                //    storedImage.sprite = orangeSprite;

                //    storedImage.gameObject.SetActive(true);
                //    G2Script.attempts += 1;

                //}
                switch (buttonName)
                {
                    case "Q1Button":
                        // Handle checking answer for Question 1
                        checkAnswerForQuestion1(userAnswerInt, storedImage);
                        break;
                    case "Q2Button":
                        // Handle checking answer for Question 2
                        checkAnswerForQuestion1(userAnswerInt, storedImage);
                        break;
                    case "Q3Button":
                        // Handle checking answer for Question 2
                        checkAnswerForQuestion1(userAnswerInt, storedImage);
                        break;
                    case "Q4Button":
                        // Handle checking answer for Question 2
                        checkAnswerForQuestion1(userAnswerInt, storedImage);
                        break;
                    case "Q5Button":
                        // Handle checking answer for Question 2
                        checkAnswerForQuestion5(userAnswerStr, storedImage);
                        break;
                    // Add cases for other buttons as needed
                    default:
                        Debug.Log("Question button not assigned a check answer method");
                        break;
                }
            }
            else
            {
                Debug.Log("Stored Image is null");
            }
        }
        else {
            Debug.Log("Button image transform is null");
        }

        
        
    }
    private string FilterCode(string[] lines)
    {
        string filteredCode = "";

        foreach (string line in lines)
        {
            // Check if the line contains "// HIDE"
            if (line.Contains("// HIDE"))
            {
                // If the line contains "// HIDE", skip it
                continue;
            }
            else
            {
                // If the line does not contain "// HIDE", include it in the filtered code
                filteredCode += line + "\n";
            }
        }

        return filteredCode;
    }
    private void checkAnswerForQuestion1(int userAnswer, Image storedImage)
    {
        if (userAnswer == result)
        {
            Debug.Log("Correct answer!");

            storedImage.sprite = correctSprite;
            storedImage.gameObject.SetActive(true);

            G2Script.totalScore += 1;
            G2Script.questionsAnsweredCorrectly += 1;


        }
        else
        {
            Debug.Log("Incorrect answer!");
            storedImage.sprite = orangeSprite;

            storedImage.gameObject.SetActive(true);
            G2Script.attempts += 1;

        }
    }
    private void checkAnswerForQuestion5(string userAnswerStr, Image storedImage)
    {
        if (userAnswerStr == result)
        {
            Debug.Log("Correct answer!");

            storedImage.sprite = correctSprite;
            storedImage.gameObject.SetActive(true);

            G2Script.totalScore += 1;
            G2Script.questionsAnsweredCorrectly += 1;


        }
        else
        {
            Debug.Log("Incorrect answer!");
            storedImage.sprite = orangeSprite;

            storedImage.gameObject.SetActive(true);
            G2Script.attempts += 1;

        }
    }

    public void ShowQ1Code()
    {
        int n = Random.Range(1, 20);
        // Check if the Q1Fib.cs script file exists
        if (File.Exists(q1ScriptPath))
        {
            result = Q1Fib.Fib(n);
            //result = Q2.
            // Read the contents of the Q1Fib.cs script file
            string code = File.ReadAllText(q1ScriptPath);

            codeText.text = code;

            questionLabel.text = "What would the result be if n was " + n + "?";

            
            codePanel.SetActive(true);
            
        }
        else
        {
            Debug.LogError("Q1Fib.cs script file not found!");
        }
    }
    public void ShowQ2Code()
    {
        int rand1 = Random.Range(1, 20);
        int rand2 = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q2ScriptPath))
        {
            //CHANGE PER QUESTION
            result = Q2.CalculateGCD(rand1, rand2);
            
            
            //CHANGE PER QUESTION
            string code = File.ReadAllText(q2ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "The numbers are " + rand1 + " " + rand2 + ", what is the result?";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q3.cs script file not found!");
        }
    }
    public void ShowQ3Code()
    {
        int a = Random.Range(1, 20);
        int b = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q3ScriptPath))
        {
            //CHANGE PER QUESTION
            result = Q3.Algorithm(a,b);

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q3ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "What is B when A is " + a + " and B is " + b;


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q4.cs script file not found!");
        }
    }
    public void ShowQ4Code()
    {
        //int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q4ScriptPath))
        {
            //CHANGE PER QUESTION
            // 5 rows of 3 O's
            result = 15;

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q4ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "How many O's are there by the end?";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ5Code()
    {
        int n = Random.Range(1, 20);
        
        
        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q5.Algorithm(n);

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q5ScriptPath);
            string filteredCode = FilterCode(lines); 

            codeText.text = filteredCode;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ6Code()
    {
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q5ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ7Code()
    {
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q5ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ8Code()
    {
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q5ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ9Code()
    {
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q5ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ10Code()
    {
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string code = File.ReadAllText(q5ScriptPath);

            codeText.text = code;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "";


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }

}
