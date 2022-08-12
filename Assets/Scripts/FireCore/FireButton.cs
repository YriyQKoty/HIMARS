using cakeslice;
using UnityEngine;

namespace DefaultNamespace.LauncherCore
{
    public class FireButton : MonoBehaviour
    {
        [SerializeField] private Transform _buttonMesh;

        [SerializeField] private LauncherController _launcherController;

        [SerializeField] private Outline _outline;

        public void Fire()
        {
            _launcherController.Fire();
        }
    }
}