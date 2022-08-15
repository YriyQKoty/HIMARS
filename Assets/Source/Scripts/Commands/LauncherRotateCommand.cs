using DG.Tweening;
using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly LauncherController _launcherController;
        
        public LauncherController.RotationData Data { get; set; }

        public LauncherRotateCommand(LauncherController launcherController,
            LauncherController.RotationData rotationData )
        {
            _launcherController = launcherController;
            Data = rotationData;
        }
        
        public void Execute()
        {
            _launcherController.HorizontalRotator.Source.Play();
            
            if (!_launcherController.FireController.IsEmpty)
            {
                _launcherController.IndicatorsController.NotReady();
            }

            _launcherController.VertRotator.Rotate(Data.VerticalAngles).OnComplete(() =>
            {
                _launcherController.VertRotator.RotationInAction = false; 
            });
            _launcherController.HorizontalRotator.Rotate(Data.HorizontalAngles).OnComplete(() =>
            {
                _launcherController.HorizontalRotator.RotationInAction = false;
                _launcherController.HorizontalRotator.Source.Stop();

                if (!_launcherController.FireController.IsEmpty)
                {
                    if (_launcherController.InDeadZone)
                    {
                        _launcherController.IndicatorsController.NotReady();
                    }
                    else
                    {
                        _launcherController.IndicatorsController.Ready();
                    }
               
                }
                else
                {
                    _launcherController.IndicatorsController.Empty();
                }

            });
        }

        public bool CanExecute()
        {
            if (!_launcherController.RotationInAction) return true;
            
            Debug.LogWarning("Rotation still in Action! Wait for finishing!");
            return false;
        }
    }
}