using System;
using System.Linq;
using DefaultNamespace.Commands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.LauncherCore
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
        
        [Header("Launcher Controller Components")]
        [SerializeField] private LauncherHorizontalRotator _horizontalRotator;
        [SerializeField] private LauncherVertRotator _vertRotator;
        [SerializeField] private FireController _fireController;

        public LauncherHorizontalRotator HorizontalRotator => _horizontalRotator;
        public FireController FireController => _fireController;

        public bool InDeadZone => _horizontalRotator.InDeadZone && _vertRotator.InDeadZone;

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
             _rotateCommand = new LauncherRotateCommand(_horizontalRotator, _vertRotator, new RotationData(Vector3.zero,Vector3.zero));
             _rotateToDefaultCommand = new LauncherRotateCommand(_horizontalRotator, _vertRotator, new RotationData(Vector3.zero,Vector3.zero));
             _cancelRotationCommand = new LauncherStopRotateCommand(_horizontalRotator, _vertRotator);

             _fireCommand = new FireCommand(_fireController, _horizontalRotator, _vertRotator);
        }

        public void RotateLauncher(RotationData rotationData)
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
            RotateLauncher(new RotationData(new Vector3(0,Random.Range(_horizontalRotator.YAngleRange.x, _horizontalRotator.YAngleRange.y),0), 
                new Vector3(Random.Range(_vertRotator.XAngleRange.x, _vertRotator.XAngleRange.y),0,0)));
        }

        public void CancelRotation()
        {
            if (_cancelRotationCommand.CanExecute())
            {
                _cancelRotationCommand.Execute();
            }
        }

        public void Fire()
        {
            if (_fireCommand.CanExecute())
            {
                _fireCommand.Execute();
            }
        }

    }
}