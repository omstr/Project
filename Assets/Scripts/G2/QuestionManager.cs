using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Transform questionsParent; // Reference to the parent object containing all question buttons
    public GameObject panelObj; // Reference to the panel object

    public Sprite greyTickSprite;
    public Image tickImage;
    public Image tickImage2;


    private Vector3[] originalPositions; // Array to store the original positions of the buttons
   

    private Vector3 offscreenPosition = new Vector3(-1000f, -1000f, 0f);
    
   

    void Start()
    {
        

        originalPositions = new Vector3[questionsParent.childCount];
        for (int i = 0; i < questionsParent.childCount; i++)
        {
            originalPositions[i] = questionsParent.GetChild(i).position;
        }

    }

    public void MoveButtonsOffscreen()
    {
        foreach (Transform button in questionsParent)
        {
            button.position = offscreenPosition;
        }
        //panelObj.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f; // Set panelObj in front of the camera
    }

    public void MoveButtonsBack()
    {
        for (int i = 0; i < questionsParent.childCount; i++)
        {
            questionsParent.GetChild(i).position = originalPositions[i];
        }
    }

    public void OnQuestionButtonClick()
    {
        MoveButtonsOffscreen();
        panelObj.SetActive(true);
        tickImage.sprite = greyTickSprite;
        tickImage2.sprite = greyTickSprite;


        //panelObj.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f; // Set panelObj in front of the camera
    }

    public void OnBackButtonClick()
    {
        MoveButtonsBack();
        panelObj.SetActive(false);
    }

}