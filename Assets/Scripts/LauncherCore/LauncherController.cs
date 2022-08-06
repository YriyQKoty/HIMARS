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

        public void RotateLauncher(RotationData rotationData)
        {
            if (_horizontalRotator.RotationInAction || _vertRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return;
            }

            ICommand cmd = new LauncherRotateCommand(_horizontalRotator, _vertRotator, rotationData);
            cmd.Execute();
        }

        public void RotateToDefault()
        {
            if (_horizontalRotator.RotationInAction || _vertRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return;
            }

            ICommand cmd = new LauncherRotateCommand(_horizontalRotator, _vertRotator, new RotationData(Vector3.zero,Vector3.zero));
            cmd.Execute();
        }

        public void RandomRotate()
        {
            RotateLauncher(new RotationData(new Vector3(0,Random.Range(_horizontalRotator.YAngleRange.x, _horizontalRotator.YAngleRange.y),0), 
                new Vector3(Random.Range(_vertRotator.XAngleRange.x, _vertRotator.XAngleRange.y),0,0)));
        }

        public void CancelRotation()
        {
            ICommand cmd = new LauncherStopRotateCommand(_horizontalRotator, _vertRotator);
            cmd.Execute();
        }

        public void Fire()
        {
            if (_horizontalRotator.RotationInAction || _vertRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return;
            }

            if (InDeadZone)
            {
                Debug.LogWarning("Launcher is in Dead zone. Rotate Launcher to fire.");
                return;
            }

            ICommand cmd = new FireCommand(_fireController);
            cmd.Execute();
            
        }

    }
}