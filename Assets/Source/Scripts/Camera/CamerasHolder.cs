using UnityEngine;

namespace Cameras
{
    public class CamerasHolder : MonoBehaviour
    {
        [SerializeField] private OutlineCamera _driverCamera;

        [SerializeField] private OutlineCamera _observerCamera;
        [SerializeField] private OutlineCamera _operatorCamera;

        public OutlineCamera DriverCamera => _driverCamera;

        public OutlineCamera ObserverCamera => _observerCamera;

        public OutlineCamera OperatorCamera => _operatorCamera;
    }
}