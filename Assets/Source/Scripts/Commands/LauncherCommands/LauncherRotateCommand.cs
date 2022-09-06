using Source.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Commands.LauncherCommands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly ILauncherRotateCommandParams _commandParams;
        public RotationData Data { get; set; }

        [Inject]
        public LauncherRotateCommand(ILauncherRotateCommandParams commandParams)
        {
            _commandParams = commandParams;
        }

        public void Execute()
        {
            _commandParams.LauncherRotator.Rotate(Data.Angles);
        }

        public bool CanExecute()
        {
            if (_commandParams.LauncherRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }
            
            if (_commandParams.IsReloading) 
            {
                Debug.LogWarning("Reloading still in Action! Wait for finishing!");
                return false;
            }
            
            return true;
        }
    }
}