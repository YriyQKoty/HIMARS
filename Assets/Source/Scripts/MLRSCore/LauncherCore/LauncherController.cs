using System;
using JetBrains.Annotations;
using Source.Scripts.Commands;
using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.Indicators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class LauncherController : MonoBehaviour
    {
        public struct RotationData
        {
            public Vector3 HorizontalAngles;
            public Vector3 VerticalAngles;

            public RotationData(Vector3 horizontalAngles, Vector3 verticalAngles)
            {
                HorizontalAngles = horizontalAngles;
                VerticalAngles = verticalAngles;
            }

            public RotationData(float horizontalAngle, float verticalAngle)
            {
                HorizontalAngles = new Vector3(0, horizontalAngle, 0);
                VerticalAngles = new Vector3(verticalAngle, 0, 0);
            }
        }

        private ICommand _rotateCommand;
        private ICommand _rotateToDefaultCommand;
        private ICommand _cancelRotationCommand;
        private ICommand _fireCommand;
        
        [Header("Rotation Components")] [Space]
        [SerializeField] private LauncherHorizontalRotator _horizontalRotator;
        [SerializeField] private LauncherVertRotator _vertRotator;
        
        [Header("Fire Components")] [Space]
        [SerializeField] private FireController _fireController;

        [Header("Indicator Components")] [Space] 
        [SerializeField] private IndicatorsController _indicatorsController;

        [Header("Trajectory & Angles")] [Space]
        [SerializeField] private AnglesDeterminator _anglesDeterminator;
        [SerializeField] private bool _shallowTrajectory = true;


        public LauncherHorizontalRotator HorizontalRotator => _horizontalRotator;
        public LauncherVertRotator VertRotator => _vertRotator;
        public FireController FireController => _fireController;
        public IndicatorsController IndicatorsController => _indicatorsController;
        public AnglesDeterminator AnglesDeterminator => _anglesDeterminator;


        public bool InDeadZone => _horizontalRotator.InDeadZone && _vertRotator.InDeadZone;

        public bool RotationInAction => _horizontalRotator.RotationInAction || _vertRotator.RotationInAction;

        public bool ShallowTrajectory => _shallowTrajectory;
        
        
        private void Awake()
        {
            if (_horizontalRotator == null) _horizontalRotator = GetComponentInChildren<LauncherHorizontalRotator>();
            if (_vertRotator == null) _vertRotator = GetComponentInChildren<LauncherVertRotator>();
        }

        private void Start()
        {
            InitCommands();
        }

        private void InitCommands()
        {
             _rotateCommand = new LauncherRotateCommand(this, new RotationData(0,0));
             _rotateToDefaultCommand = new LauncherRotateCommand(this, new RotationData(0,0));
             _cancelRotationCommand = new LauncherStopRotateCommand(this);

             _fireCommand = new FireCommand(this, _anglesDeterminator,
                 new FireData(_anglesDeterminator.PointOfAiming.position, _vertRotator.transform, 0));
        }

        public void Rotate(RotationData rotationData)
        {
            if (_rotateCommand.CanExecute())
            {
                ((LauncherRotateCommand)_rotateCommand).Data = rotationData;
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

        public void RandomRotate()
        {
            Rotate(new RotationData(Random.Range(_horizontalRotator.YAngleRange.x, _horizontalRotator.YAngleRange.y), 
               Random.Range(_vertRotator.XAngleRange.x, _vertRotator.XAngleRange.y)));
        }

        public void CancelRotation()
        {
            if (_cancelRotationCommand.CanExecute())
            {
                _cancelRotationCommand.Execute();
            }
        }

        public void ToggleTrajectory()
        {
            _shallowTrajectory = !_shallowTrajectory;
        }

        public void Fire([CanBeNull] Action callback = null)
        {
            if (!_fireCommand.CanExecute()) return;

            ((FireCommand)_fireCommand).Data.Target = _anglesDeterminator.PointOfAiming.position;
            ((FireCommand)_fireCommand).Data.Angle = _vertRotator.transform.localEulerAngles.x;
            
            _fireCommand.Execute();

            callback?.Invoke();
        }

    }
}