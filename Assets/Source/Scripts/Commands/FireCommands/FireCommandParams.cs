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
        FireController FireController { get; }
        float DistanceToTarget { get; }
        bool RotationInAction { get; }
        bool InDeadZone { get; }
        
        Vector3 PointOfAimingPosition { get; }
        float CurrentXAngle { get; }
        
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
        
        public FireCommandParams(FireController fireController,
            LauncherRotator launcherRotator,
            AnglesDeterminator anglesDeterminator)
        {
            _fireController = fireController;
            _launcherRotator = launcherRotator;
            _anglesDeterminator = anglesDeterminator;

        }
        
        public FireController FireController => _fireController;

        public float DistanceToTarget => _anglesDeterminator.DistanceToTarget;

        public Vector3 PointOfAimingPosition => _anglesDeterminator.PointOfAiming.position;

        public float CurrentXAngle => _launcherRotator.CurrentXAngle;

        public Transform VerticalRotTransform => _launcherRotator.VerticalRotTransform;

        public bool RotationInAction => _launcherRotator.RotationInAction;
        public bool InDeadZone => _launcherRotator.InDeadZone;
    }
 
}