using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands.LauncherCommands
{
    public class LauncherRotateCommand : ICommand
    {
        private readonly ILauncherRotateCommandParams _commandParams;
        public RotationData Data { get; set; }

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
            if (!_commandParams.LauncherRotator.RotationInAction) return true;
            
            Debug.LogWarning("Rotation still in Action! Wait for finishing!");
            return false;
        }
    }
}