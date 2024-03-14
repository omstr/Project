using Assets.Scripts.G3;
using UnityEngine;

public class L5Script : Level
{
    public override bool PreCompilationValidate(string code)
    {
        if (code.Contains("for")) return true;
        if (code.Contains("while")) return true;
        if (code.Contains("do")) return true;
        return false;
    }
}
