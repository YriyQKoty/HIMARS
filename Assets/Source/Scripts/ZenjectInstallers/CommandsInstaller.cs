using Source.Scripts.Commands;
using Source.Scripts.Commands.FireCommands;
using Source.Scripts.Commands.LauncherCommands;
using Source.Scripts.Commands.ReloadCommands;
using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using Source.Scripts.MovementCore;
using UnityEngine;
using Zenject;

namespace Source.Scripts.ZenjectInstallers
{
    public class CommandsInstaller : MonoInstaller
    {
        [SerializeField] private LauncherRotator _launcherRotator;
        [SerializeField] private FireController _fireController;
        [SerializeField] private AmmoReloader _ammoReloader;
        [SerializeField] private AnglesDeterminator _anglesDeterminator;
        [SerializeField] private VehicleController _vehicleController;

        public override void InstallBindings()
        {
            Container.Bind<TubesContainer>().FromComponentInChildren().AsTransient();
            Container.Bind<VehicleController>().FromInstance(_vehicleController).AsTransient();
            
            Container.Bind<FireController>().FromInstance(_fireController).AsTransient().NonLazy();
            Container.Bind<LauncherRotator>().FromInstance(_launcherRotator).AsTransient().NonLazy();
            Container.Bind<AmmoReloader>().FromInstance(_ammoReloader).AsTransient().NonLazy();
            Container.Bind<AnglesDeterminator>().FromInstance(_anglesDeterminator).AsTransient().NonLazy();

            Container.Bind<CommandsController>().AsTransient().NonLazy();

            //commands invokers
            Container.Bind<ICommandsInvoker>().WithId(nameof(LauncherRotationCommandsInvoker))
                .To<LauncherRotationCommandsInvoker>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<ICommandsInvoker>().WithId(nameof(FireCommandsInvoker))
                .To<FireCommandsInvoker>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<ICommandsInvoker>().WithId(nameof(ReloadCommandsInvoker))
                .To<ReloadCommandsInvoker>()
                .AsTransient()
                .NonLazy();
            
            //commands params
            Container.Bind<ILauncherRotateCommandParams>().To<LauncherRotateCommandParams>()
                .AsTransient()
                .WithConcreteId(nameof(LauncherRotateCommandParams))
                .NonLazy();
            
            Container.Bind<IFireCommandParams>().To<FireCommandParams>()
                .AsTransient()
                .WithConcreteId(nameof(FireCommandParams))
                .NonLazy();
            
            Container.Bind<IReloadCommandParams>().To<ReloadCommandParams>()
                .AsTransient()
                .WithConcreteId(nameof(ReloadCommandParams))
                .NonLazy();

            //commands
            Container.Bind<FireCommand>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<LauncherRotateCommand>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<LauncherCancelRotateCommand>()
                .AsTransient()
                .NonLazy();
            
            Container.Bind<ReloadCommand>()
                .AsTransient()
                .NonLazy();
        }
    }
}