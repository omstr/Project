using System.Collections;

namespace Assets.Scripts.G3
{

    public interface ITankControls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public IEnumerator Run();
        public void MoveForward();
        public void MoveBackward();
        public void RotateLeft();
        public void RotateRight();

        public void SpinTurret(float angle);
        public void Fire();
    }
}
