using Assets.Scripts.G3;
using TMPro;
using UnityEngine;

public class L3Script : Level
{
    TextMeshProUGUI requirementsText;
    private void Awake()
    {
        requirementsText = transform.Find("requirementsText").GetComponent<TextMeshProUGUI>();
    }
    public override bool PreCompilationValidate(string code)
    {
        requirementsText.text = "reach the target by using a while loop";

        if (code.Contains("while")) return true;
        return false;
    }
}
