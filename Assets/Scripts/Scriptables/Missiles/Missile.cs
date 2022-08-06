using UnityEngine;

namespace DefaultNamespace.Scriptables.Missiles
{
    [CreateAssetMenu(fileName = "Missile data", menuName = "MLRS/Missile", order = 1)]
    public class Missile : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private AudioClip _sound;

        [Header("Deviation params")] 
        [SerializeField] private float _deviationSpeed;
        [SerializeField] private float _deviationAmount;
        [SerializeField] private float _timePercentage;

        public float TimePercentage => _timePercentage;

        public float DeviationSpeed => _deviationSpeed;

        public float DeviationAmount => _deviationAmount;

        public float Speed => _speed;

        public AudioClip Sound => _sound;
    }
}