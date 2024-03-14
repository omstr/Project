using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class CodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText; // text component to display the code
    public Button q1Button; // change to button array 
    public Button q2Button; // change to button array 
    public Button q3Button; // change to button array 
    public Button q4Button; // change to button array 
    public Button q5Button; // change to button array 
    public Button q6Button; // change to button array 
    public Button q7Button; // change to button array 
    public Button q8Button; // change to button array 
    public Button q9Button; // change to button array 
    public Button q10Button; // change to button array 

    public Button[] questionButtons;

    public GameObject codePanel; 
    public TextMeshProUGUI questionLabel;
    public TextMeshProUGUI scoreDisplay;
    public TMP_InputField userInputField;
    public Image buttonImage;
    public Image tickImage;
    public Image tickImage2;
    public Sprite correctSprite;
    public Sprite orangeSprite;
    public Sprite redSprite;
    public Sprite greyTickSprite;
    public Sprite greenTickSprite;


    

    public int result; // for int comparisons

    public string strResult; // for string comparisons

    public string[] expectedStrings;

    private bool questionAlreadyAnswered = false;
    private bool isStringPartCorrect = false;
    private bool isIntPartCorrect = false;

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
        tickImage.gameObject.SetActive(true);
        tickImage2.gameObject.SetActive(false);
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
    public void SetQuestionAnsweredFalse()
    {
        questionAlreadyAnswered = false;
    }
    public void CheckAnswer()
    {
        // Get the reference to the button that was clicked from other class.
        Button selectedButton = ButtonSelector.selectedButton;


        // DONE TODO: ONLY ACCEPTS NUMBERS RIGHT NOW
        // SHOULD PROBABLY START A SWITCH CASE IN A METHOD FOR DIFF TYPES OF Qs

        //int userAnswerInt = int.Parse(userInputField.text);
        //string userAnswerStr = userInputField.text;


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
                        checkAnswerForIntQuestion( storedImage);
                        break;
                    case "Q2Button":
                        // Handle checking answer for Question 2
                        checkAnswerForIntQuestion( storedImage);
                        break;
                    case "Q3Button":
                        // Handle checking answer for Question 3
                        checkAnswerForIntQuestion(storedImage);
                        break;
                    case "Q4Button":
                        // Handle checking answer for Question 4
                        checkAnswerForIntQuestion( storedImage);
                        break;
                    case "Q5Button":
                        // Handle checking answer for Question 5
                        checkAnswerForQuestion5(storedImage);
                        break;
                    case "Q6Button":
                        // Handle checking answer for Question 6
                        checkAnswerForIntQuestion(storedImage);
                        break;
                    case "Q7Button":
                        // Handle checking answer for Question 7
                        CheckAnswerForStringAndIntQuestion(storedImage);
                        break;
                    case "Q8Button":
                        // Handle checking answer for Question 8
                        
                        break;
                    // Add cases for other buttons 
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
    private void checkAnswerForIntQuestion(Image storedImage)
    {
        int userAnswerInt = int.Parse(userInputField.text);
        if (userAnswerInt == result)
        {
            tickImage.sprite = greenTickSprite;
            Debug.Log("Correct answer!");

            storedImage.sprite = correctSprite;
            storedImage.gameObject.SetActive(true);
            if (questionAlreadyAnswered == false)
            {
                G2Script.totalScore += 1;
                G2Script.questionsAnsweredCorrectly += 1;
            }
            scoreDisplay.text = "Points: " + G2Script.totalScore;

            questionAlreadyAnswered = true;
        }
        else
        {
            tickImage.sprite = greyTickSprite;
            Debug.Log("Incorrect answer!");
            storedImage.sprite = redSprite;

            storedImage.gameObject.SetActive(true);
            G2Script.attempts += 1;
            Debug.Log(result);

        }
    }
    private void checkAnswerForQuestion5(Image storedImage)
    {
        // convert the list to a single string separated by commas and trim spaces

        string userAnswerStr = userInputField.text;

        string newUserAnswerStr = userAnswerStr.Replace(" ", "");


        if (newUserAnswerStr.Equals(strResult))
        {
            Debug.Log("Correct answer!");
            tickImage.sprite = greenTickSprite;
            storedImage.sprite = correctSprite;
            storedImage.gameObject.SetActive(true);
            if (questionAlreadyAnswered == false)
            {
                G2Script.totalScore += 1;
                G2Script.questionsAnsweredCorrectly += 1;
            }
            
            scoreDisplay.text = "Points: " + G2Script.totalScore;
            //Debug.Log(strResult);
            questionAlreadyAnswered = true;
        }
        else
        {
            tickImage.sprite = greyTickSprite;
            Debug.Log("Incorrect answer!");
            storedImage.sprite = redSprite;

            storedImage.gameObject.SetActive(true);
            G2Script.attempts += 1;
            //Debug.Log(strResult);
        }
    }
    private void CheckAnswerForStringAndIntQuestion(Image storedImage)
    {
        string userInput = userInputField.text;
        string[] parts = userInput.Split(',');
        //string stringPart = parts[0].Trim();
        //string intPart = parts[1].Trim();


        bool hasAnsweredString = false;
        bool hasAnsweredInt = false;
        

        // Check if the input has two parts
        if (parts.Length == 2)
        {
            string userInputString = parts[0].Trim();
            int userInputInt;
            bool parsedSuccessfully = int.TryParse(parts[1].Trim(), out userInputInt);

            if (parsedSuccessfully)
            {
                // Compare string and integer parts with expected values
                bool intPartCorrect = userInputInt == result;
                bool stringPartCorrect = false;
                foreach (string expectedString in expectedStrings)
                {
                    if (parts[0].Equals(expectedString))
                    {
                        stringPartCorrect = true;
                        break;
                    }
                }
                

                if (stringPartCorrect)
                {
                    Debug.Log("Correct string part!");
                    //G2Script.totalScore += 1; // Increase score for correct string part


                    //G2Script.questionsAnsweredCorrectly += 1;
                    
                    scoreDisplay.text = "Points: " + G2Script.totalScore;
                    storedImage.sprite = orangeSprite;
                    tickImage.sprite = greenTickSprite;
                    tickImage2.sprite = greyTickSprite;

                }
                if (intPartCorrect)
                {
                    Debug.Log("Correct integer part!");
                    //G2Script.totalScore += 1; // Increase score for correct integer part
                   
                    scoreDisplay.text = "Points: " + G2Script.totalScore;
                    storedImage.sprite = orangeSprite;
                    tickImage2.sprite = greenTickSprite;
                    tickImage.sprite = greyTickSprite;
                }

                // Update UI based on correctness of parts
                if (stringPartCorrect && intPartCorrect)
                {
                    Debug.Log("Correct answer!");
                    if(questionAlreadyAnswered == false)
                    {
                        G2Script.totalScore += 2;
                        G2Script.questionsAnsweredCorrectly += 2;
                        G2Script.sessionQsAnswered += 2;
                    }
                    
                    scoreDisplay.text = "Points: " + G2Script.totalScore;
                    storedImage.sprite = correctSprite;
                    storedImage.gameObject.SetActive(true);
                    tickImage.sprite = greenTickSprite;
                    tickImage2.sprite = greenTickSprite;
                    questionAlreadyAnswered = true;


                }
                else
                {
                    Debug.Log("Incorrect answer!");
                    storedImage.sprite = redSprite;
                    storedImage.gameObject.SetActive(true);
                    G2Script.attempts += 1;
                    G2Script.sessionQsAnswered += 1;
                    //tickImage.sprite = greyTickSprite;
                    //tickImage2.sprite = greyTickSprite;
                }
            }
            else
            {
                Debug.Log("Invalid input format. Please enter a string followed by a number separated by a comma.");
            }
        }
        else if (parts.Length == 1)
        {
            string userInputString = parts[0].Trim();

            //// Check if the input is a valid integer
            //if (int.TryParse(userInputString, out int userInputInt))
            //{
            //    bool intPartCorrect = userInputInt == result;
            //    if (intPartCorrect)
            //    {
            //        Debug.Log("Correct integer part!");
            //        G2Script.totalScore += 1; // Increase score for correct integer part

            //        Debug.Log("Correct answer!");
            //        tickImage2.sprite = greenTickSprite;
            //        tickImage.sprite = greyTickSprite;
            //        storedImage.sprite = orangeSprite;
            //        storedImage.gameObject.SetActive(true);
            //        G2Script.questionsAnsweredCorrectly += 1;

            //        scoreDisplay.text = "Points: " + G2Script.totalScore;
            //    }
            //    else
            //    {
            //        tickImage2.sprite = greyTickSprite;
            //        Debug.Log("Incorrect integer part!");
            //        storedImage.sprite = redSprite;
            //        storedImage.gameObject.SetActive(true);
            //        G2Script.attempts += 1;

            //    }
            //}
            //else
            //{
            //    bool stringPartCorrect = false;
            //    foreach (string expectedString in expectedStrings)
            //    {
            //        if (userInputString.Equals(expectedString))
            //        {
            //            stringPartCorrect = true;
            //            break;
            //        }
            //    }
            //    if (stringPartCorrect)
            //    {
            //        Debug.Log("Correct string part!");
            //        tickImage.sprite = greenTickSprite;
            //        tickImage2.sprite = greyTickSprite;
            //        G2Script.totalScore += 1; // Increase score for correct string part
            //        storedImage.sprite = orangeSprite;
            //        storedImage.gameObject.SetActive(true);
            //        G2Script.questionsAnsweredCorrectly += 1;

            //        scoreDisplay.text = "Points: " + G2Script.totalScore;
            //    }
            //    else
            //    {
            //        Debug.Log("Incorrect string part!");
            //        tickImage.sprite = greyTickSprite;
            //        storedImage.sprite = redSprite;
            //        storedImage.gameObject.SetActive(true);
            //        G2Script.attempts += 1;

            //        foreach (string expectedString in expectedStrings)
            //        {
            //            Debug.Log("string: "+ expectedString);
            //        }
            //    }
            //}
            // Check if the input is a valid integer
            
        }
        else
        {
            Debug.Log("Invalid input format. Please enter either a string, or an int. Or both a string followed by a number separated by a comma.");
        }
    }
    public void ShowQ1Code()
    {
        tickImage2.gameObject.SetActive(false);
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
        tickImage2.gameObject.SetActive(false);
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
        tickImage2.gameObject.SetActive(false);
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
        tickImage2.gameObject.SetActive(false);
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
        tickImage2.gameObject.SetActive(false);
        int n = Random.Range(1, 20);
        
        
        //CHANGE PER QUESTION
        if (File.Exists(q5ScriptPath))
        {
            //CHANGE PER QUESTION
            strResult = Q5.Algorithm(n);

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q5ScriptPath);
            string filteredCode = FilterCode(lines); 

            codeText.text = filteredCode;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "Enter your answer for the result, seperated by commas. n = " + n;


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q5.cs script file not found!");
        }
    }
    public void ShowQ6Code()
    {
        tickImage2.gameObject.SetActive(false);
        int x = Random.Range(1, 5);
        int y = Random.Range(1, 10);

        string strX = x.ToString();
        string strY = y.ToString();

        //CHANGE PER QUESTION
        if (File.Exists(q6ScriptPath))
        {
            //CHANGE PER QUESTION
            result = Q6.Algorithm(strX, strY);

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q6ScriptPath);
            string filteredCode = FilterCode(lines);

            codeText.text = filteredCode;

            //TODO: CHANGE PER QUESTION
            questionLabel.text = "What is total when input1 is " + x + " and input2 is " + y;


            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q6.cs script file not found!");
        }
    }
    public void ShowQ7Code()
    {
        tickImage2.gameObject.SetActive(true);
        int randNum;
        //CHANGE PER QUESTION
        if (File.Exists(q7ScriptPath))
        {

            //CHANGE PER QUESTION
            
            randNum = Random.Range(3, 8);
            //randNum = 6; //TODO Remove
            Q7.InitialiseCAndB(randNum);
            //Debug.Log(Q7.A);
            //Debug.Log(Q7.B);
            //Debug.Log(Q7.C);
            //Q7.Adjust();
            //Debug.Log(Q7.A);
            //Debug.Log(Q7.B);
            //Debug.Log(Q7.C);
            while (Q7.B < Q7.A)
            {
                
                Q7.Output1();
                Q7.Output2();
                Q7.Adjust();
                //Debug.Log("A: " + Q7.A);
                //Debug.Log("B: " + Q7.B);
                //Debug.Log("C: " + Q7.C);
            }


            expectedStrings = new string[] { "triangle", "pyramid" };
            result = Q7.listOfOs.Count; // The amount of O's in the list

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q7ScriptPath);
            string filteredCode = FilterCode(lines);

            codeText.text = filteredCode;
            Debug.Log("O count: " + result);
            //foreach (string str in expectedStrings)
            //{
            //    Debug.Log(str);
            //}
            string convertedString = string.Join(",", Q7.listOfOs);
            foreach (char ch in convertedString)
            {
                Debug.Log(ch + ", ");
            }
            //TODO: CHANGE PER QUESTION
            questionLabel.text = "What will this output look like? What is the amount of O's printed? randNum = " + randNum + "\n \n Hint: enter in string,int format.";

            codePanel.SetActive(true);

        }
        else
        {
            Debug.LogError("Q7.cs script file not found!");
        }
    }
    public void ClearQ7List()
    {
        Q7.listOfOs.Clear();
    }
    public void ShowQ8Code()
    {
        tickImage2.gameObject.SetActive(false);
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q8ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q8ScriptPath);
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
    public void ShowQ9Code()
    {
        tickImage2.gameObject.SetActive(false);
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q9ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q9ScriptPath);
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
    public void ShowQ10Code()
    {
        tickImage2.gameObject.SetActive(false);
        int n = Random.Range(1, 20);

        //CHANGE PER QUESTION
        if (File.Exists(q10ScriptPath))
        {
            //CHANGE PER QUESTION
            //result = Q2

            //CHANGE PER QUESTION
            string[] lines = File.ReadAllLines(q10ScriptPath);
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

}
