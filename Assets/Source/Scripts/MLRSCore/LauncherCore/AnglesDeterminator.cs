using System;
using UnityEngine;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class AnglesDeterminator : MonoBehaviour
    {
        public Transform PointOfAiming;
        [SerializeField] private LauncherController _launcherController;
        
        public float DistanceToTarget => Vector3.Distance(transform.position, PointOfAiming.position);

        private float _horAngle;
        private float _vertAngle;
        

        public void DetermineAngles()
        {
            _horAngle =
                -Vector3.SignedAngle(_launcherController.HorizontalRotator.transform.position - PointOfAiming.position,transform.forward, Vector3.up);

            var distanceToTarget = DistanceToTarget;

            var maxDistanceTimePeak = Vector3.Distance(transform.position,
                transform.forward * _launcherController.FireController.CurrentMissileCharacteristics.MaxDistanceTimePeak);

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
                if (90 - vertAngleLess45 <= _launcherController.VertRotator.XAngleRange.y)
                {
                    vertAngleGreater45 = 90 - vertAngleLess45;
                }
                else
                {
                    if (!_launcherController.ShallowTrajectory)
                    {
                        _launcherController.ToggleTrajectory();
                    }
                }
                
                _vertAngle = _launcherController.ShallowTrajectory ? vertAngleLess45 : vertAngleGreater45;
            }
            
        }

        public void RotateToTarget()
        {
            DetermineAngles();
            _launcherController.Rotate(new LauncherController.RotationData(_horAngle,_vertAngle));
        }


    }
}