using Assets.Scripts.G3;
using UnityEngine;

public class L2Script : Level
{
    public override bool PreCompilationValidate(string code)
    {
        if (code.Contains("for")) return true;
        return false;
    }
}
