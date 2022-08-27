using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands
{
    public class FireCommand : ICommand
    {
        private readonly LauncherRotator _launcherRotator;
        private readonly FireController _fireController;
        private readonly AnglesDeterminator _anglesDeterminator;
        
        public FireData Data;
        
        public FireCommand( LauncherRotator launcherRotator, FireController fireController, 
            AnglesDeterminator anglesDeterminator, 
            FireData data)
        {
            _launcherRotator = launcherRotator;
            _fireController = fireController;
            _anglesDeterminator = anglesDeterminator;
            Data = data;
        }
        
        public void Execute()
        {
            _fireController.Fire(Data);
        }

        public bool CanExecute()
        {
            if (_launcherRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }

            if (_launcherRotator.InDeadZone)
            {
                Debug.LogWarning("Launcher is in Dead zone. Rotate Launcher to fire.");
                return false;
            }

            if ( _fireController.IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return false;
            }

            if ( _fireController.IsEmpty)
            {
                Debug.LogWarning("All tubes are empty! Cannot fire!");
                return false;
            }

            //if distance to target is greater than maximum range (always when angle is 45 deg)
            if (_anglesDeterminator.DistanceToTarget >
                _fireController.CurrentMissileCharacteristics.MaximumRange)
            {
                Debug.LogWarning("Unreachable target! Cannot fire!");
                return false;
            }
            
            return true;
        }
    }
}