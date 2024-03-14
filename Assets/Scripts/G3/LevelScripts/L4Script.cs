using Assets.Scripts.G3;
using UnityEngine;

public class L4Script : Level
{
    public override bool PreCompilationValidate(string code)
    {
        if (code.Contains("do")) return true;
        return false;
    }
}
