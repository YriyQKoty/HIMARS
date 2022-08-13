using System.Collections.Generic;
using MissileCore;
using UnityEngine;

namespace MLRSCore.FireCore
{
    public class FireTube : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private List<ParticleSystem> _particles;

        [Header("Missile")] [SerializeField] private MissileController missileController;

        [SerializeField] private float _delay = 0.5f;

        private void Start()
        {
            _source.clip = AudioManager.Instance.CurrentMlrs.PickRandomFireSound();
        }

        public float Delay => _delay;
        public bool IsReady { get; private set; } = true;

        public void Fire(Vector3 target)
        {
            IsReady = false;
            
            _source.PlayOneShot(_source.clip);
            
            missileController.Launch(Vector3.back);
            
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
    }
}