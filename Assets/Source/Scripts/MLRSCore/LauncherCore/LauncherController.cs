using System;
using Commands;
using JetBrains.Annotations;
using MLRSCore.FireCore;
using MLRSCore.Indicators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MLRSCore.LauncherCore
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


        public LauncherHorizontalRotator HorizontalRotator => _horizontalRotator;
        public LauncherVertRotator VertRotator => _vertRotator;
        public FireController FireController => _fireController;
        public IndicatorsController IndicatorsController => _indicatorsController;


        public bool InDeadZone => _horizontalRotator.InDeadZone && _vertRotator.InDeadZone;

        public bool RotationInAction => _horizontalRotator.RotationInAction || _vertRotator.RotationInAction;
        
        
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
             _rotateCommand = new LauncherRotateCommand(this, new RotationData(Vector3.zero,Vector3.zero));
             _rotateToDefaultCommand = new LauncherRotateCommand(this, new RotationData(Vector3.zero,Vector3.zero));
             _cancelRotationCommand = new LauncherStopRotateCommand(this);

             _fireCommand = new FireCommand(this);
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
            Rotate(new RotationData(new Vector3(0,Random.Range(_horizontalRotator.YAngleRange.x, _horizontalRotator.YAngleRange.y),0), 
                new Vector3(Random.Range(_vertRotator.XAngleRange.x, _vertRotator.XAngleRange.y),0,0)));
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
            
            _fireCommand.Execute();

            callback?.Invoke();
        }

    }
}