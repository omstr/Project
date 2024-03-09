using UnityEngine;

namespace Assets.Scripts.G3
{
    public abstract class Level : MonoBehaviour
    {
        public abstract bool PreCompilationValidate(string code);
    }
}
