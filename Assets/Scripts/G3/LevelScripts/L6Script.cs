using Assets.Scripts.G3;
using UnityEngine;

public class L6Script : Level
{
    public override bool PreCompilationValidate(string code)
    {
        if (code.Contains("int[]")) return true;
        return false;
    }
}
