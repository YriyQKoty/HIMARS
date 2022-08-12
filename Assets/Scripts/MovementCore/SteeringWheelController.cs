using System;
using DefaultNamespace.Scriptables.Vehicle;
using UnityEngine;

namespace DefaultNamespace.MovementCore
{
    public class SteeringWheelController : MonoBehaviour
    {
        [SerializeField] private Transform _steer;

        [SerializeField] private Vehicle _vehicle;

        private Vector3 _initialRotation;

        private void Start()
        {
            _initialRotation = _steer.localRotation.eulerAngles;
        }

        public void Rotate(float angle)
        {
            var yAngle = _steer.localRotation.eulerAngles.y;

            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;

            if (angle == 0)
            {
                var delta = (yAngle - _initialRotation.y) * Time.fixedDeltaTime * _vehicle.SteerSpeed * 0.5f;
                
                _steer.Rotate(0, -delta, 0, Space.Self);
            }
            else if (angle > 0)
            {
                if (yAngle < _vehicle.MaxSteeringAngle * 2)
                {
                    _steer.Rotate(0, angle * _vehicle.SteerSpeed * 0.5f,0, Space.Self);
                }
            }
            else
            {
                if (yAngle > -(_vehicle.MaxSteeringAngle * 2)) 
                {
                    _steer.Rotate(0, angle * _vehicle.SteerSpeed * 0.5f,0, Space.Self);
                }
            }

        }
    }
}