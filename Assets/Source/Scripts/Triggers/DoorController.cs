using Source.Scripts.Camera;
using Source.Scripts.Commands;
using Source.Scripts.Commands.OtherCommands;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Triggers
{
    public class DoorController : SeatController
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Transform _parentForPlayer;
        void Awake()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
        
        }

        public void Open()
        {
            ICommand cmd = new OpenDoorCommand(_animator);
            cmd.Execute();
        }

        public override void Enter(Transform player)
        {
            Entered = true;
            OnEnterInvoke();

            Open();

            ChangeCameraOnSeat();
        
            player.parent = _parentForPlayer;
        }

        public override void Exit(Transform player)
        {
            Entered = false;
            OnExitInvoke();
        
            Open();
        
            CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Default);
        
            player.parent = null;
        }
    }

}