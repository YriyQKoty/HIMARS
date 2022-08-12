using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class OpenDoorCommand : ICommand
    {
        private Animator _animator;
        
        public OpenDoorCommand(Animator animator)
        {
            _animator = animator;
        }
        
        public void Execute()
        {
            _animator.Play(AnimationStateNames.OpenDoor);
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}