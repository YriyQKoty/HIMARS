using UnityEngine;

namespace Source.Scripts.Scriptables.Missiles
{
    [CreateAssetMenu(fileName = "Missile data", menuName = "MLRS/Missile", order = 1)]
    public class Missile : ScriptableObject
    {
        [Header("Sounds")][Space]
        [SerializeField] private AudioClip _flyingSound;
        [SerializeField] private AudioClip _explosionSound;

        [Header("Explosion chars")] [Space]
        [SerializeField] private float _explosionForce;

        [SerializeField] private float _explosionRadius;
        
        [Header("Ballistic chars")] [Space]
        [SerializeField] private float _maxDistanceTimePeak;
        [SerializeField] private float _initialVelocity;
        [SerializeField] private float _deviationRadius;
        
        public float MaxDistanceTimePeak => _maxDistanceTimePeak;

        public float MaximumRange => _maxDistanceTimePeak * 2;

        public float InitialVelocity => _initialVelocity;

        public float ExplosionForce => _explosionForce;

        public float ExplosionRadius => _explosionRadius;

        public float DeviationRadius => _deviationRadius;

        public AudioClip FlyingSound => _flyingSound;
    }
}