using Commands;
using DefaultNamespace;
using UnityEngine;

public class DoorController : ExitablePartController
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

        Open();

        ChangeCameraOnSeat();
        
        player.parent = _parentForPlayer;
    }

    public override void Exit(Transform player)
    {
        Entered = false;
        
        Open();
        
        CamerasManager.Instance.ChangeCamera(CamerasManager.CameraType.Default);
        
        player.parent = null;
    }
}
