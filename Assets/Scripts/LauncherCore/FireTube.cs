using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.LauncherCore
{
    public class FireTube : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private ParticleSystem _particle;

        private void Start()
        {
            _source.clip = AudioManager.Instance.PickRandomFireSound();
        }

        public float Delay => _source.clip.length;
        public bool IsReady { get; private set; } = true;

        public void Fire()
        {
            IsReady = false;
            
            _source.PlayOneShot(_source.clip);
            _particle.Play();
        }
    }
}