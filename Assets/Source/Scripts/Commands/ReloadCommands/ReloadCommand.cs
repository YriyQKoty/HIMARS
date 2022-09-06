using DG.Tweening;
using Source.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Commands.ReloadCommands
{
    public class ReloadCommand : ICommand
    {
        private readonly IReloadCommandParams _reloadCommandParams;

        [Inject]
        public ReloadCommand(IReloadCommandParams reloadCommandParams)
        {
            _reloadCommandParams = reloadCommandParams;
        }

        public void Execute()
        {
            _reloadCommandParams.AmmoReloader.StartReloading();
            _reloadCommandParams.LauncherRotator.Rotate(new Vector3(0,180,0));
            DOVirtual.DelayedCall(_reloadCommandParams.LauncherRotator.RotTime, () =>
            {
                _reloadCommandParams.AmmoReloader.Reload();
            }, false);
            
        }

        public bool CanExecute()
        {
            if (_reloadCommandParams.LauncherRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }
            
            if (  _reloadCommandParams.IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return false;
            }

            if ( !_reloadCommandParams.IsEmpty)
            {
                Debug.LogWarning("Not all tubes are empty! Cannot reload!");
                return false;
            }
            
            if (_reloadCommandParams.AmmoReloader.IsReloading) 
            {
                Debug.LogWarning("Reloading still in Action! Wait for finishing!");
                return false;
            }

            return true;
        }
    }
}