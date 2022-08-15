using cakeslice;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class ExitablePartController : MonoBehaviour, IExitable
    {
        [Header("Outline props")][Space]
        [SerializeField] protected Outline _outline;
        
        
        [SerializeField] protected MeshRenderer _meshRenderer;

        [Header("Seat prop")][Space]
        [SerializeField] private SeatType _seat;

        public bool Entered { get; protected set; }

        public Outline Outline => _outline;

        public MeshRenderer MeshRenderer => _meshRenderer;

        public abstract void Enter(Transform player);
        
        public abstract void Exit(Transform player);

        public void ChangeCameraOnSeat()
        {
            switch (_seat)
            {
                case SeatType.None: CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Default);
                    break;
                case SeatType.Driver: CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Driver);
                    break;
                case SeatType.Operator: CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Operator);
                    break;
                case SeatType.Observer: CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Observer);
                    break;
                case SeatType.Passenger: Debug.LogError("Not Implemented!");
                    break;
                default: CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Default);
                    break;
            }
        }
    }
}