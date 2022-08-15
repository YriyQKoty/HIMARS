using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FPSController _fpsController;

        public enum ViewMode
        {
            FPS,
            Third
        }

        public void SwitchMode(ViewMode viewMode)
        {
            if (viewMode == ViewMode.FPS)
            {
                _fpsController.enabled = true;
            }
        }
    }
}