using Source.Scripts.Helpers;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Commands
{
    public class OpenDoorCommand : ICommand
    {
        private readonly Animator _animator;
        
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