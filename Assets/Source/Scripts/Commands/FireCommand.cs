using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands
{
    public class FireCommand : ICommand
    {
        private readonly LauncherController _launcherController;
        private readonly AnglesDeterminator _anglesDeterminator;

        private Vector3 _target = Vector3.zero;
        public FireData Data;
        
        public FireCommand(LauncherController launcherController, AnglesDeterminator anglesDeterminator, FireData data)
        {
            _launcherController = launcherController;
            _anglesDeterminator = anglesDeterminator;
            Data = data;
        }
        
        public void Execute()
        {
            _launcherController.FireController.Fire(Data);
        }

        public bool CanExecute()
        {
            if (_launcherController.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }

            if (_launcherController.InDeadZone)
            {
                Debug.LogWarning("Launcher is in Dead zone. Rotate Launcher to fire.");
                return false;
            }

            if (_launcherController.FireController.IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return false;
            }

            if (_launcherController.FireController.IsEmpty)
            {
                Debug.LogWarning("All tubes are empty! Cannot fire!");
                return false;
            }

            //if distance to target is greater than maximum range (always when angle is 45 deg)
            if (_anglesDeterminator.DistanceToTarget >
                _launcherController.FireController.CurrentMissileCharacteristics.MaximumRange)
            {
                Debug.LogWarning("Unreachable target! Cannot fire!");
                return false;
            }
            
            return true;
        }
    }
}