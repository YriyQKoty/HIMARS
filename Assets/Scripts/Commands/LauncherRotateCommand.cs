using DefaultNamespace.LauncherCore;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly LauncherHorizontalRotator _horizontalRotator;
        private readonly LauncherVertRotator _vertRotator;
        private readonly LauncherController.RotationData _data;

        public LauncherRotateCommand(LauncherHorizontalRotator horizontalRotator, 
            LauncherVertRotator vertRotator,
            LauncherController.RotationData rotationData)
        {
            _horizontalRotator = horizontalRotator;
            _vertRotator = vertRotator;
            _data = rotationData;
        }
        
        public void Execute()
        {
            _vertRotator.Rotate(_data.VerticalAngles).OnComplete(() =>
            {
                _vertRotator.RotationInAction = false; 
            });
            _horizontalRotator.Rotate(_data.HorizontalAngles).OnComplete(() =>
            {
                _horizontalRotator.RotationInAction = false;
            });
        }
    }
}