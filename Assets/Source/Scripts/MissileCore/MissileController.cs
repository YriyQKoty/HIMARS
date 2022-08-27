using System;
using DG.Tweening;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.Scriptables.Missiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.MissileCore
{
    public class MissileController : MonoBehaviour
    {
        [Header("Physics components")]
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        
        [Header("Mesh")]
        [SerializeField] private GameObject _mesh;

        [Header("Missile data")] [SerializeField]
        private Missile _missile;
        
        [Header("Effects")] [SerializeField] private ParticleSystem _particle;

        private bool _launched = false;
        private Vector3 _target;
        private float _angle;
      
        private Collider[] _colliders = new Collider[10];

        public Missile Missile => _missile;

        private void Awake()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(FireData data)
        {
            _target = data.Target;
            _angle = data.Angle;
            
            _mesh.transform.localScale = Vector3.zero;
            _mesh.SetActive(true);

            _mesh.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
            {
                _collider.enabled = true;
            });
            gameObject.transform.SetParent(null);

            _rigidbody.isKinematic = false;
            _launched = true;

            _particle.Play();
            Shoot();

        }

        private void Shoot()
        {
            var fromTo = new Vector3(
                Random.Range(_target.x - _missile.DeviationRadius, _target.x + _missile.DeviationRadius), 
                _target.y,
                Random.Range(_target.z - _missile.DeviationRadius, _target.z + _missile.DeviationRadius)) 
                         - transform.position;
            
            var fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

            var x = fromToXZ.magnitude;
            var y = fromTo.y;

            var v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(_angle * Mathf.Deg2Rad) * x) *
                                                      Mathf.Pow(Mathf.Cos(_angle * Mathf.Deg2Rad), 2));

            var v = Mathf.Sqrt(Mathf.Abs(v2));
            
            _rigidbody.AddForce(transform.forward * v, ForceMode.Impulse);
          
        }
        
        private void Update()
        {
            if (!_launched) return;

            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //todo effects
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, _missile.ExplosionRadius, _colliders);
            for (int i = 0; i < size; i++)
            {
                var rg = _colliders[i].GetComponent<Rigidbody>();
                if (rg != null)
                {
                    rg.AddExplosionForce(_missile.ExplosionForce, transform.position, _missile.ExplosionRadius);
                }
            }

            _launched = false;
            _mesh.transform.DOScale(Vector3.zero, 0.02f).OnComplete(() =>
            {
                _collider.enabled = false;
                _rigidbody.isKinematic = true;
            });
        }
    }
}