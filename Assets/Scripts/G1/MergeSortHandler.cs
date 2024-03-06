using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortHandler : MonoBehaviour
{
    private TextMeshProUGUI output;
    private InputField textInputField;
    private TextMeshProUGUI sortedOutput;
    private Button enterButton;
    private string inputString;
    private float delayInSeconds = 1f;
    public CubeGenerator cubeGenerator;
    public GameObject prismObject;
    public Slider delaySlider;
    private List<int> intList = new List<int>();
    private List<int> sortedList = new List<int>();
    public Image tickImage;
    public Image crossImage;
    private int maxNumbers = 9;
    private TextMeshProUGUI questionLabel;
    private TextMeshProUGUI scoreLabel;
    private InputHandler iHandler;
    private bool isSorting = false;
    private RectTransform CubeGenObj;

    private Coroutine clearCoroutine;
    private Coroutine sortingCoroutine;
    private void Awake()
    {
        output = transform.Find("outputLabel").GetComponent<TextMeshProUGUI>();
        textInputField = transform.Find("legacyInput").GetComponent<InputField>();
        sortedOutput = transform.Find("sortedLabel").GetComponent<TextMeshProUGUI>();
        enterButton = transform.Find("enterButton").GetComponent<Button>();
        questionLabel = transform.Find("QuestionLabel").GetComponent<TextMeshProUGUI>();
        scoreLabel = transform.Find("scoreLabel").GetComponent<TextMeshProUGUI>();
        CubeGenObj = transform.Find("CubeGen").GetComponent<RectTransform>();

        iHandler = transform.GetComponent<InputHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //cubeGenerator = CubeGenObj.GetComponent<CubeGenerator>();
        //cubeGenerator = GameObject.GetComponent<CubeGenerator>();



        delaySlider.minValue = 0.1f;
        delaySlider.value = 1;
    }

    public void StartSteppedMergeSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        if (!isSorting)
        {
            isSorting = true;
            sortingCoroutine = StartCoroutine(SteppedMergeSortCoroutine(list, onComplete));
        }
    }
    public IEnumerator SteppedMergeSortCoroutine(List<int> list, Action<List<int>> onComplete)
    {
        CubeGenerator cubeGenerator = new CubeGenerator();
        List<int> sortedList = new List<int>(list);
        List<int> temp = new List<int>(list);
        int size = sortedList.Count;
        float adjustedDelay = delayInSeconds; //* delaySlider.value;

        prismObject = GameObject.Find("Prism");
        List<GameObject> cubeObjects = cubeGenerator.grabCubes();
        questionLabel.text = "";
        questionLabel.gameObject.SetActive(true);

        yield return MergeSortRecursive(sortedList, temp, 0, size - 1, adjustedDelay, cubeObjects);

        Debug.Log("Sorting Complete");

        // Invoke the callback with the sorted list
        onComplete?.Invoke(sortedList);
    }

    IEnumerator MergeSortRecursive(List<int> list, List<int> temp, int left, int right, float delay, List<GameObject> cubeObjects)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;

            yield return MergeSortRecursive(list, temp, left, mid, delay, cubeObjects);
            yield return MergeSortRecursive(list, temp, mid + 1, right, delay, cubeObjects);

            yield return Merge(list, temp, left, mid, right, delay, cubeObjects);
        }
    }

    IEnumerator Merge(List<int> list, List<int> temp, int left, int mid, int right, float delay, List<GameObject> cubeObjects)
    {
        int i = left;
        int j = mid + 1;
        int k = left;

        // Merge the two sorted arrays into temp[]
        while (i <= mid && j <= right)
        {
            if (list[i] <= list[j])
            {
                temp[k] = list[i];
                i++;
            }
            else
            {
                temp[k] = list[j];
                j++;
            }
            k++;
        }

        // Copy the remaining elements of left[], if any
        while (i <= mid)
        {
            temp[k] = list[i];
            i++;
            k++;
        }

        // Copy the remaining elements of right[], if any
        while (j <= right)
        {
            temp[k] = list[j];
            j++;
            k++;
        }

        // Copy the elements of temp[] back into list[]
        for (int x = left; x <= right; x++)
        {
            list[x] = temp[x];
        }

        // Update the cubes to visually represent the merging process
        for (int x = left; x <= right; x++)
        {
            iHandler.MoveTextAboveHighlightedObject(cubeObjects[x], cubeObjects[x]);
            iHandler.MovePrismAboveHighlightedObject(cubeObjects[x]);
            yield return new WaitForSeconds(delay);
        }
    }

}
