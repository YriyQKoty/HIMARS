using Commands;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
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

}
