using System.Collections.Generic;
using UnityEngine;

namespace MovementCore
{
    public class WheelsController : MonoBehaviour
    {
        [SerializeField] private List<Wheel> _wheels;
        [SerializeField] private Wheel _rightFrontWheel;
        [SerializeField] private Wheel _leftFrontWheel;

        public Wheel RightFrontWheel => _rightFrontWheel;

        public Wheel LeftFrontWheel => _leftFrontWheel;

        void Start()
        {
            Brake();
        }

        public void Brake()
        {
            _leftFrontWheel.Collider.brakeTorque = float.MaxValue - 1;
            _rightFrontWheel.Collider.brakeTorque = float.MaxValue - 1;
        
            foreach (var wheel in _wheels)
            {
                wheel.Collider.brakeTorque = float.MaxValue - 1;
            }
        }

        public void StartMove()
        {
            _leftFrontWheel.Collider.brakeTorque = 0;
            _rightFrontWheel.Collider.brakeTorque = 0;
        
            foreach (var wheel in _wheels)
            {
                wheel.Collider.brakeTorque = 0;
            }
        }


        public void UpdateWheels()
        {
            _leftFrontWheel.UpdateWheel();
            _rightFrontWheel.UpdateWheel();
        
            foreach (var wheel in _wheels)
            {
                wheel.UpdateWheel();
            }
        }
    }
}

