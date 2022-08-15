using UnityEngine;

namespace Source.Scripts.Scriptables.Vehicle
{
    [CreateAssetMenu(fileName = "Vehicle data", menuName = "MLRS/Vehicle", order = 0)]
    public class Vehicle : ScriptableObject
    {
        [Header("Velocity & Power")] [Space]
        [SerializeField] private float _maxFrontSpeedKph;

        [SerializeField] private float _maxBackSpeedKph;

        [SerializeField] private float _motorPower;

        [Header("Steering")] [Space]
        [SerializeField] private float _minSteerAngle;
        [SerializeField] private float _maxSteeringAngle;

        [SerializeField] private float _steerSpeed;


        public float MaxFrontSpeedKph => _maxFrontSpeedKph;

        public float MaxBackSpeedKph => _maxBackSpeedKph;
        public float MotorPower => _motorPower;

        
        public float MinSteerAngle => _minSteerAngle;
        public float MaxSteeringAngle => _maxSteeringAngle;
        public float SteerSpeed => _steerSpeed;


    }
}