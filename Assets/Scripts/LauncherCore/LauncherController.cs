using System;
using DefaultNamespace.Commands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.LauncherCore
{
    public class LauncherController : MonoBehaviour
    {
        public enum ExecutionOrder
        {
            HorizontalFirst,
            VerticalFirst
        }
        public struct RotationData
        {
            public Vector3 HorizontalAngles;
            public Vector3 VerticalAngles;
            public ExecutionOrder Order;

            public RotationData(Vector3 horizontalAngles, Vector3 verticalAngles, ExecutionOrder order)
            {
                HorizontalAngles = horizontalAngles;
                VerticalAngles = verticalAngles;
                Order = order;
            }
        }
        
        [SerializeField] private LauncherHorizontalRotator _horizontalRotator;
        [SerializeField] private LauncherVertRotator _vertRotator;

        private void Awake()
        {
            if (_horizontalRotator == null) _horizontalRotator = GetComponentInChildren<LauncherHorizontalRotator>();
            if (_vertRotator == null) _vertRotator = GetComponentInChildren<LauncherVertRotator>();
        }

        public void RotateLauncher(Vector3 horizontalAngles, Vector3 vertAngles, ExecutionOrder order = ExecutionOrder.HorizontalFirst)
        {
            if (_horizontalRotator.RotationInAction || _vertRotator.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return;
            }

            ICommand cmd = new LauncherRotateCommand(_horizontalRotator,
                _vertRotator,
                new RotationData(horizontalAngles, vertAngles, order));
            cmd.Execute();
        }

        public void RotateToDefault()
        {
            RotateLauncher(Vector3.zero,Vector3.zero, ExecutionOrder.VerticalFirst);
        }

        public void RandomRotate()
        {
            RotateLauncher(new Vector3(0,Random.Range(_horizontalRotator.YAngleRange.x, _horizontalRotator.YAngleRange.y),0), 
                new Vector3(Random.Range(_vertRotator.XAngleRange.x, _vertRotator.XAngleRange.y),0,0));
        }

        public void StopRotation()
        {
            ICommand cmd = new LauncherStopRotateCommand(_horizontalRotator, _vertRotator);
            cmd.Execute();
        }

    }
}