using DG.Tweening;
using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly LauncherRotator _launcherRotator;
        
        public LauncherController.RotationData Data { get; set; }

        public LauncherRotateCommand(LauncherRotator launcherRotator,
            LauncherController.RotationData rotationData )
        {
            _launcherRotator = launcherRotator;
            Data = rotationData;
        }
        
        public void Execute()
        {
            _launcherRotator.Rotate(Data.Angles);
        }

        public bool CanExecute()
        {
            if (!_launcherRotator.RotationInAction) return true;
            
            Debug.LogWarning("Rotation still in Action! Wait for finishing!");
            return false;
        }
    }
}