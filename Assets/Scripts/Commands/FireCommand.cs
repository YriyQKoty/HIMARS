using DefaultNamespace.LauncherCore;
using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class FireCommand : ICommand
    {
        private readonly FireController _fireController;
        private readonly LauncherHorizontalRotator _horizontalRotator;
        private readonly LauncherVertRotator _vertRotator;
        
        private Vector3 _target = Vector3.zero;
        
        public FireCommand(FireController fireController, LauncherHorizontalRotator horizontalRotator, LauncherVertRotator vertRotator)
        {
            _fireController = fireController;
            _horizontalRotator = horizontalRotator;
            _vertRotator = vertRotator;
        }
        
        public void Execute()
        {
            _fireController.Fire(_target);
        }

        public bool CanExecute()
        {
            if (_horizontalRotator.RotationInAction || _vertRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }

            if (_horizontalRotator.InDeadZone && _vertRotator.InDeadZone)
            {
                Debug.LogWarning("Launcher is in Dead zone. Rotate Launcher to fire.");
                return false;
            }

            if (_fireController.IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return false;
            }

            if (_fireController.IsEmpty)
            {
                Debug.LogWarning("All tubes are empty! Cannot fire!");
                return false;
            }
            
            return true;
        }
    }
}