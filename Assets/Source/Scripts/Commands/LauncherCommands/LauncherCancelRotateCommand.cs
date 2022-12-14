using Source.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Commands.LauncherCommands
{
    public class LauncherCancelRotateCommand : ICommand
    {
        private readonly ILauncherRotateCommandParams _commandParams;
        
        [Inject]
        public LauncherCancelRotateCommand(ILauncherRotateCommandParams commandParams)
        {
            _commandParams = commandParams;
        }
        public void Execute()
        {
            _commandParams.LauncherRotator.Stop();
        }

        public bool CanExecute()
        {
            if (_commandParams.IsReloading) 
            {
                Debug.LogWarning("Reloading still in Action! Wait for finishing!");
                return false;
            }
            
            return true;
        }
    }
}