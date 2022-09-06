using System;
using Source.Scripts.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.Commands.LauncherCommands
{
    public struct RotationData
    {
        public Vector3 Angles;

        public RotationData(Vector3 angles)
        {
            Angles = angles;
        }

        public RotationData(float horizontalAngle, float verticalAngle)
        {
            Angles = new Vector3(verticalAngle, horizontalAngle, 0);
        }
    }

    public sealed class LauncherRotationCommandsInvoker : ICommandsInvoker
    {

        private readonly LauncherRotateCommand _rotateCommand;
        private readonly LauncherCancelRotateCommand _cancelRotationCommand;

        private readonly ILauncherRotateCommandParams _commandParams;
        
        [Inject]
        public LauncherRotationCommandsInvoker(ILauncherRotateCommandParams commandParams)
        {
            _commandParams = commandParams;
            _rotateCommand = new LauncherRotateCommand(commandParams);
            _cancelRotationCommand = new LauncherCancelRotateCommand(commandParams);
        }

        public void InvokeCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.RotateLauncherToDefault: Rotate(default);
                    break;
                case CommandType.RotateLauncherToTarget: Rotate(new RotationData(_commandParams.TargetAngles));
                    break;
                case CommandType.CancelLauncherRotation: Cancel();
                    break;
                case CommandType.RotateLauncherRandom: Rotate(new RotationData(
                        Random.Range(_commandParams.LauncherRotator.YAngleRange.x, _commandParams.LauncherRotator.YAngleRange.y), 
                    Random.Range(_commandParams.LauncherRotator.XAngleRange.y, _commandParams.LauncherRotator.XAngleRange.y)));
                    break;
             default:
                    throw new Exception("This invoker cannot execute such a command!");
            }
        }

        private void Rotate(RotationData rotationData)
        {
            if (_rotateCommand.CanExecute())
            {
                _rotateCommand.Data = rotationData;
                _rotateCommand.Execute();
            };
        }

        private void Cancel()
        {
            if (_cancelRotationCommand.CanExecute())
            {
                _cancelRotationCommand.Execute();
            };
        }
    }
}