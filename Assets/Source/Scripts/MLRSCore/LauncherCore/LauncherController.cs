using System;
using JetBrains.Annotations;
using Source.Scripts.Commands;
using Source.Scripts.MLRSCore.FireCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class LauncherController : MonoBehaviour
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
        
     
   

        private LauncherRotateCommand _rotateCommand;
        private LauncherRotateCommand _rotateToDefaultCommand;
        private LauncherCancelRotateCommand _cancelRotationCommand;
        private FireCommand _fireCommand;
        
        [Header("Rotation Components")] [Space]

        [SerializeField] private LauncherRotator _launcherRotator;
        
        [Header("Fire Components")] [Space]
        [SerializeField] private FireController _fireController;
        
        [Header("Trajectory & Angles")] [Space]
        [SerializeField] private AnglesDeterminator _anglesDeterminator;
        public FireController FireController => _fireController;


        private void Start()
        {
            InitCommands();
        }

        private void InitCommands()
        {
             _rotateCommand = new LauncherRotateCommand(_launcherRotator, new RotationData(0,0));
             _rotateToDefaultCommand = new LauncherRotateCommand(_launcherRotator, new RotationData(0,0));
             _cancelRotationCommand = new LauncherCancelRotateCommand(_launcherRotator);

             _fireCommand = new FireCommand(_launcherRotator, _fireController, _anglesDeterminator,
                 new FireData(_anglesDeterminator.PointOfAiming.position, _launcherRotator.VerticalRotTransform, 0));
        }

        public void Rotate(RotationData rotationData)
        {
            if (_rotateCommand.CanExecute())
            {
                _rotateCommand.Data = rotationData;
                _rotateCommand.Execute();
            };
        }

        public void RotateToDefault()
        {
            if (_rotateToDefaultCommand.CanExecute())
            {
                _rotateToDefaultCommand.Execute();
            }
        }

        public void RotateToTarget()
        {
           Rotate(new RotationData(_anglesDeterminator.DetermineAngles()));
        }

        public void RandomRotate()
        {
            Rotate(new RotationData(Random.Range(_launcherRotator.YAngleRange.x, _launcherRotator.YAngleRange.y), 
               Random.Range(_launcherRotator.XAngleRange.x, _launcherRotator.XAngleRange.y)));
        }

        public void CancelRotation()
        {
            if (_cancelRotationCommand.CanExecute())
            {
                _cancelRotationCommand.Execute();
            }
        }

        public void Fire([CanBeNull] Action callback = null)
        {
            if (!_fireCommand.CanExecute()) return;

            _fireCommand.Data.Target = _anglesDeterminator.PointOfAiming.position;
            _fireCommand.Data.Angle = _launcherRotator.CurrentXAngle;
            
            _fireCommand.Execute();

            callback?.Invoke();
        }

    }
}