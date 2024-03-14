using Assets.Scripts.G3;
using UnityEngine;

public class L7Script : Level
{
    public override bool PreCompilationValidate(string code)
    {
        if (code.Contains("while")) return false;
        if (code.Contains("for")) return false;
        if (code.Contains("do")) return false;
        return true;
    }
}
