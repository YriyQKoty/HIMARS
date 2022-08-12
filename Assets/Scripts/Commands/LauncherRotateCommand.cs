using DefaultNamespace.LauncherCore;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly LauncherHorizontalRotator _horizontalRotator;
        private readonly LauncherVertRotator _vertRotator;
        public LauncherController.RotationData Data { get; set; }

        public LauncherRotateCommand(LauncherHorizontalRotator horizontalRotator, 
            LauncherVertRotator vertRotator,
            LauncherController.RotationData rotationData)
        {
            _horizontalRotator = horizontalRotator;
            _vertRotator = vertRotator;
            Data = rotationData;
        }
        
        public void Execute()
        {
            _horizontalRotator.Source.Play();
            
            _vertRotator.Rotate(Data.VerticalAngles).OnComplete(() =>
            {
                _vertRotator.RotationInAction = false; 
            });
            _horizontalRotator.Rotate(Data.HorizontalAngles).OnComplete(() =>
            {
                _horizontalRotator.RotationInAction = false;
                _horizontalRotator.Source.Stop();
            });
        }

        public bool CanExecute()
        {
            if (!_horizontalRotator.RotationInAction && !_vertRotator.RotationInAction) return true;
            
            Debug.LogWarning("Rotation still in Action! Wait for finishing!");
            return false;

        }
    }
}