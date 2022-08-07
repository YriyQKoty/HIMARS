using System;
using System.Collections.Generic;
using DefaultNamespace.Scriptables.Vehicle;
using UnityEngine;

namespace DefaultNamespace.MovementCore
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private WheelsController _wheelsController;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Transform COM;

        [SerializeField] private Vehicle _vehicle;

        [SerializeField] private bool _wheelsInverted = true;

        private float _horizontalInput = 0;
        private float _verticalInput = 0;

        private float _steeringAngle = 0;

        private void Awake()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            GetInput();

            _rigidbody.centerOfMass = COM.localPosition;
            Steer();
            Accelerate();
            _wheelsController.UpdateWheels();
        }

        public void GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");

            _verticalInput =  _wheelsInverted ?  -Input.GetAxis("Vertical") : Input.GetAxis("Vertical");
            
            if (_verticalInput == 0)
            {
                _wheelsController.Brake();
            }
            else
            {
                _wheelsController.StartMove();
            }
          
        }

        public void Steer()
        {
            var maxSpeedKph = GetMaxSpeed();
            
            float speedFactor =_rigidbody.velocity.sqrMagnitude / maxSpeedKph ;

            _steeringAngle = Mathf.Lerp(_vehicle.MaxSteeringAngle, _vehicle.MinSteerAngle, speedFactor) * _horizontalInput;
            _wheelsController.LeftFrontWheel.Collider.steerAngle = Mathf.Lerp(_wheelsController.LeftFrontWheel.Collider.steerAngle, _steeringAngle, 
                Time.fixedDeltaTime * _vehicle.SteerSpeed) ;
            _wheelsController.RightFrontWheel.Collider.steerAngle =  Mathf.Lerp(_wheelsController.RightFrontWheel.Collider.steerAngle, _steeringAngle,
                Time.fixedDeltaTime * _vehicle.SteerSpeed);
        }
        
        
        public void Accelerate()
        {
            var maxSpeedKmph = GetMaxSpeed();
            
            //checking squared in kph to prevent using magnitude 
            if (_rigidbody.velocity.sqrMagnitude * 3.6f * 3.6f > maxSpeedKmph * maxSpeedKmph)
            {
                _wheelsController.LeftFrontWheel.Collider.motorTorque = 0 ;
                _wheelsController.RightFrontWheel.Collider.motorTorque = 0 ;
            }
            else
            {
                _wheelsController.LeftFrontWheel.Collider.motorTorque = _verticalInput * _vehicle.MotorPower ;
                _wheelsController.RightFrontWheel.Collider.motorTorque = _verticalInput *_vehicle.MotorPower ;
            }
        }

        private float GetMaxSpeed()
        {
            return _wheelsInverted ? (
                    _verticalInput <= 0 ? _vehicle.MaxFrontSpeedKph : _vehicle.MaxBackSpeedKph) : 
                _verticalInput < 0 ? _vehicle.MaxBackSpeedKph : _vehicle.MaxFrontSpeedKph;
        }
        
    }
}