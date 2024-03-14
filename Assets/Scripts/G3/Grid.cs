using Assets.Scripts.G3;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    private DialogManager dialogManager;
    [SerializeField]
    private int gridWidth = 11, gridHeight = 11;

    [SerializeField]
    private GridItem[] items;

    private CodeManager cm;
    private int checkpoints = 0, targets = 0;

    private void Awake()
    {
        //dialogManager = transform.Find("DialogManager").GetComponent<DialogManager>();
    }
    private void Start()
    {
        cm = GameObject.Find("Code").GetComponent<CodeManager>();

        ResetGrid();
    }

    public void ResetGrid()
    {
        checkpoints = 0; 
        targets = 0;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            var itemObject = Instantiate(item.prefab, transform, false);
            itemObject.transform.localPosition = item.position;

            if (item.type == GridItem.ItemType.Controller) cm.SetTankObj(itemObject);
            else if (item.type == GridItem.ItemType.Checkpoint) checkpoints++;
            else if (item.type == GridItem.ItemType.Target) targets++;

            item.ActiveObject = itemObject;
        }
    }

    // Could be improved!
    public GridItem ItemAt(Vector3 position)
    {
        foreach (var item in items)
        {
            if (item.position.x == position.x && item.position.y == position.y) return item;
        }

        return null;
    }

    public void Moved(Vector3 position)
    {
        var item = ItemAt(position);
        if (item == null) return;

        if (item.type == GridItem.ItemType.Checkpoint)
        {
            checkpoints--;
            item.ActiveObject.SetActive(false);
            G3Script.sessionQsAnswered += 1;
            G3Script.totalScore += 1;
            G3Script.questionsAnsweredCorrectly += 1;
            Debug.Log("Checkpoint Hit! " + checkpoints + " Left!");
        }
        else if (item.type == GridItem.ItemType.Target)
        {
            targets--;
            item.ActiveObject.SetActive(false);
            G3Script.sessionQsAnswered += 1;
            G3Script.totalScore += 1;
            G3Script.questionsAnsweredCorrectly += 1;
            Debug.Log("target Hit! " + targets + " Left!");
        }
        else if (item.type == GridItem.ItemType.Finish)
        {
            if (checkpoints == 0 && targets == 0)
            {
                item.ActiveObject.SetActive(false);
                //EditorUtility.DisplayDialog("Game Won!", "Game Won!", "OK");
                //dialogManager.ShowDialog("Game Won!");
                G3Script.sessionQsAnswered += 1;
                G3Script.totalScore += 1;
                G3Script.questionsAnsweredCorrectly += 1;
                EndGame(true);
            }
            else
            {
                item.ActiveObject.SetActive(false);
                //dialogManager.ShowDialog("Game Lost!");
                //EditorUtility.DisplayDialog("Game Lost", "Game Lost! " + targets + " Targets And " + checkpoints + " Checkpoints Are Left!", "OK");
                Debug.Log("Game Semi-Lost! ");
                G3Script.sessionQsAnswered += 1;
                G3Script.totalScore += 1;
                G3Script.questionsAnsweredCorrectly += 1;
                EndGame(true);
            }

        }
    }

    public void ReduceTarget()
    {
        targets--;
    }

    public bool CanMove(Vector3 position)
    {
        var item = ItemAt(position);
        if (item == null) return CheckBounds(position);
        else return CheckBounds(position) && item.type != GridItem.ItemType.Blocker && item.type != GridItem.ItemType.HalfBlocker;
    }

    public bool CheckBounds(Vector3 position)
    {
        if (position.x >= gridWidth || position.y >= gridHeight || position.x < 0 || position.y < 0) return false;
        return true;
    }

    void EndGame(bool won)
    {
        cm.ForceStop();

        if (won)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "Game3")
            {
                SceneManager.LoadScene("G3L2");
            }
            else if (currentSceneName.StartsWith("G3L"))  // Check if the scene name starts with "G3L"
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                // Increment the scene index to switch to the next level (e.g., from "G3L2" to "G3L3")
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            //SceneManager.LoadScene() Load the scene after this one, make it work dynamically
            // Code to Move to Next Level!
        }
    }
}
