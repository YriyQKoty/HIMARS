using DG.Tweening;
using Scriptables.Missiles;
using UnityEngine;

namespace MissileCore
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

        private void Awake()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 targetPos)
        {
            _mesh.transform.localScale = Vector3.zero;
            _mesh.SetActive(true);

            _mesh.transform.DOScale(Vector3.one, 0.1f);
            gameObject.transform.SetParent(null);

            _rigidbody.isKinematic = false;
            _launched = true;

            _particle.Play();

        }

        private void AddDeviation()
        {
            var deviation = new Vector3(Mathf.Cos(Time.time * _missile.DeviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _missile.DeviationAmount * _missile.TimePercentage;

            var _deviatedPrediction = _rigidbody.position + _rigidbody.velocity + predictionOffset;
            
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
          
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 45 * Time.deltaTime));
        }

        private void FixedUpdate()
        {
            if (!_launched) return;

            _rigidbody.velocity = transform.forward * _missile.Speed;
            
            AddDeviation();
        }
    }
}