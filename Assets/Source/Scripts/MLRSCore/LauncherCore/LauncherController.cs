using System;
using JetBrains.Annotations;
using Source.Scripts.Commands.FireCommands;
using Source.Scripts.Commands.LauncherCommands;
using Source.Scripts.Commands.ReloadCommands;
using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using UnityEngine;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class LauncherController : MonoBehaviour
    {
        [Header("Rotation Components")] [Space] [SerializeField]
        private LauncherRotator _launcherRotator;

        [Header("Fire Components")] [Space] [SerializeField]
        private FireController _fireController;

        [SerializeField] private AmmoReloader _ammoReloader;

        [Header("Trajectory & Angles")] [Space] [SerializeField]
        private AnglesDeterminator _anglesDeterminator;

        public FireController FireController => _fireController;

        private ICommandsInvoker _launcherRotationCommandsInvoker;
        private ICommandsInvoker _fireCommandsInvoker;
        private ICommandsInvoker _reloadCommandsInvoker;

        private void Start()
        {
            InitCommandInvokers();
        }

        private void InitCommandInvokers()
        {
            _launcherRotationCommandsInvoker =
                new LauncherRotationCommandsInvoker(new LauncherRotateCommandParams(_launcherRotator,_ammoReloader,_anglesDeterminator));
            _fireCommandsInvoker =
                new FireCommandsInvoker(new FireCommandParams(_fireController,_launcherRotator,_anglesDeterminator,_ammoReloader));

            _reloadCommandsInvoker = new ReloadCommandsInvoker(new ReloadCommandParams(_ammoReloader,_launcherRotator,_fireController));
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

        public void Fire([CanBeNull] Action callback = null)
        {
            _fireCommandsInvoker.InvokeCommand(CommandType.FireOne);

            callback?.Invoke();
        }
    }
}