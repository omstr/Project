using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogManager : MonoBehaviour
{
    private RectTransform dialogPanel;
    private TextMeshProUGUI dialogText;
    private float dialogDuration = 5f; // Duration of the dialog in seconds
    private void Awake()
    {
        dialogPanel = transform.Find("DialogPanel").GetComponent<RectTransform>();
        dialogText = dialogPanel.Find("DialogText").GetComponent<TextMeshProUGUI>();
    }
    public void ShowDialog(string message)
    {
        dialogText.text = message;
        dialogPanel.gameObject.SetActive(true);
        StartCoroutine(HideDialogAfterDelay());
    }

    private IEnumerator HideDialogAfterDelay()
    {
        yield return new WaitForSeconds(dialogDuration);
        CloseDialog();
    }

    public void CloseDialog()
    {
        dialogPanel.gameObject.SetActive(false);
    }
}
