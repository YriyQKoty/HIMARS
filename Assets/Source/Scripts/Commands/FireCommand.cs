using Interfaces;
using MLRSCore.FireCore;
using MLRSCore.LauncherCore;
using UnityEngine;

namespace Commands
{
    public class FireCommand : ICommand
    {
        private readonly LauncherController _launcherController;

        private Vector3 _target = Vector3.zero;
        
        public FireCommand(LauncherController launcherController)
        {
            _launcherController = launcherController;
        }
        
        public void Execute()
        {
            _launcherController.FireController.Fire(_target);
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
            
            return true;
        }
    }
}