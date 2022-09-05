using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.Commands.FireCommands
{
    /// <summary>
    /// Interface of FireCommand params
    /// </summary>
    public interface IFireCommandParams
    {
        //Receiver
        FireController FireController { get; }
        
        //Params for CanExecute checking
        float DistanceToTarget { get; }
        bool RotationInAction { get; }
        bool InDeadZone { get; }
        
        Vector3 PointOfAimingPosition { get; }
        float CurrentXAngle { get; }
        bool IsReloading { get; }
        
        Transform VerticalRotTransform { get; }
    };
        


    /// <summary>
    /// Implementation of default FireCommand Params
    /// </summary>
    public class FireCommandParams : IFireCommandParams
    {
        private readonly FireController _fireController;
        private readonly LauncherRotator _launcherRotator;
        private readonly AnglesDeterminator _anglesDeterminator;
        private readonly AmmoReloader _ammoReloader;
        
        public FireCommandParams(FireController fireController,
            LauncherRotator launcherRotator,
            AnglesDeterminator anglesDeterminator,
            AmmoReloader ammoReloader)
        {
            _fireController = fireController;
            _launcherRotator = launcherRotator;
            _anglesDeterminator = anglesDeterminator;
            _ammoReloader = ammoReloader;
        }
        
        public FireController FireController => _fireController;

        public bool IsReloading => _ammoReloader.IsReloading;

        public float DistanceToTarget => _anglesDeterminator.DistanceToTarget;

        public Vector3 PointOfAimingPosition => _anglesDeterminator.PointOfAiming.position;

        public float CurrentXAngle => _launcherRotator.CurrentXAngle;

        public Transform VerticalRotTransform => _launcherRotator.VerticalRotTransform;

        public bool RotationInAction => _launcherRotator.RotationInAction;
        public bool InDeadZone => _launcherRotator.InDeadZone;
    }
 
}