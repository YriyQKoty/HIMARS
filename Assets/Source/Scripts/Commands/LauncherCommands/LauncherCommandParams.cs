using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands.LauncherCommands
{
    public interface ILauncherRotateCommandParams
    {
        //Receiver
        LauncherRotator LauncherRotator { get; }
        
        //Params
        bool IsReloading { get; }
        Vector3 TargetAngles { get; }
        
    }

    public class LauncherRotateCommandParams : ILauncherRotateCommandParams
    {
        private readonly LauncherRotator _launcherRotator;
        private readonly AmmoReloader _ammoReloader;
        private readonly AnglesDeterminator _anglesDeterminator;

        public LauncherRotateCommandParams(LauncherRotator launcherRotator, AmmoReloader ammoReloader, AnglesDeterminator anglesDeterminator)
        {
            _launcherRotator = launcherRotator;
            _ammoReloader = ammoReloader;
            _anglesDeterminator = anglesDeterminator;
        }

        public bool IsReloading => _ammoReloader.IsReloading;

        public LauncherRotator LauncherRotator => _launcherRotator;

        public Vector3 TargetAngles => _anglesDeterminator.DetermineAngles();
    }
}