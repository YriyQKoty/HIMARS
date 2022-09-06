using System;
using JetBrains.Annotations;
using Source.Scripts.Commands.FireCommands;
using Source.Scripts.Commands.LauncherCommands;
using Source.Scripts.Commands.ReloadCommands;
using Source.Scripts.Interfaces;
using Zenject;

namespace Source.Scripts.Commands
{
    public class CommandsController
    {
        private readonly ICommandsInvoker _launcherRotationCommandsInvoker;
        private readonly ICommandsInvoker _fireCommandsInvoker;
        private readonly ICommandsInvoker _reloadCommandsInvoker;

        [Inject]
        public CommandsController([Inject (Id = nameof(LauncherRotationCommandsInvoker))]ICommandsInvoker launcherRotationCommandsInvoker, 
            [Inject (Id = nameof(FireCommandsInvoker))]ICommandsInvoker fireCommandsInvoker, 
            [Inject (Id = nameof(ReloadCommandsInvoker))]ICommandsInvoker reloadCommandsInvoker)
        {
            _launcherRotationCommandsInvoker = launcherRotationCommandsInvoker;
            _fireCommandsInvoker = fireCommandsInvoker;
            _reloadCommandsInvoker = reloadCommandsInvoker;
        }

        public void RotateToDefault()
        {
            _launcherRotationCommandsInvoker.InvokeCommand(CommandType.RotateLauncherToDefault);
        }

        public void RotateToTarget()
        {
            _launcherRotationCommandsInvoker.InvokeCommand(CommandType.RotateLauncherToTarget);
        }

        public void RandomRotate()
        {
            _launcherRotationCommandsInvoker.InvokeCommand(CommandType.RotateLauncherRandom);
        }

        public void CancelRotation()
        {
            _launcherRotationCommandsInvoker.InvokeCommand(CommandType.CancelLauncherRotation);
        }

        public void Reload()
        {
            _reloadCommandsInvoker.InvokeCommand(CommandType.Reload);
        }

        public void Fire()
        {
            _fireCommandsInvoker.InvokeCommand(CommandType.FireOne);
        }
    }
}