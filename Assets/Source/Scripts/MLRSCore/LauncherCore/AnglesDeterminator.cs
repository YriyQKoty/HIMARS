using System;
using Source.Scripts.MLRSCore.FireCore;
using UnityEngine;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class AnglesDeterminator : MonoBehaviour
    {
        public Transform PointOfAiming;
        [SerializeField] private LauncherRotator _launcherRotator;
        [SerializeField] private FireController _fireController;
        
        [Header("Trajectory")] [Space]
        [SerializeField] private bool _shallowTrajectory = true;
        
        public bool ShallowTrajectory => _shallowTrajectory;
        
        public float DistanceToTarget => Vector3.Distance(transform.position, PointOfAiming.position);

        private float _horAngle;
        private float _vertAngle;
        

        public Vector2 DetermineAngles()
        {
            _horAngle =
                -Vector3.SignedAngle(_launcherRotator.HorizontalRotTransform.position - PointOfAiming.position,transform.forward, Vector3.up);

            var distanceToTarget = DistanceToTarget;

            var maxDistanceTimePeak = Vector3.Distance(transform.position,
                transform.forward * _fireController.CurrentMissileCharacteristics.MaxDistanceTimePeak);

            //If max range is needed, than angle should be 45 deg
            if (distanceToTarget >= (maxDistanceTimePeak * 2))
            {
                _vertAngle = 45;
            }
            else
            {
                //else angle can be chosen from two ([0;45] deg or [45; max Vert angle])
                var vertAngleLess45 = 45 * distanceToTarget / (maxDistanceTimePeak * 2);
                var vertAngleGreater45 = 0f;
                if (90 - vertAngleLess45 <= _launcherRotator.XAngleRange.y)
                {
                    vertAngleGreater45 = 90 - vertAngleLess45;
                }
                else
                {
                    if (!ShallowTrajectory)
                    {
                        _shallowTrajectory = !_shallowTrajectory;
                    }
                }
                
                _vertAngle = ShallowTrajectory ? vertAngleLess45 : vertAngleGreater45;
            }
            
            return new Vector2(_vertAngle, _horAngle);
            
        }
        
    }
}