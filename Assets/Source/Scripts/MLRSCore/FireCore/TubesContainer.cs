using System.Collections.Generic;
using Source.Scripts.Helpers.Pool;
using Source.Scripts.ObjectPools;
using UnityEngine;

namespace Source.Scripts.MLRSCore.FireCore
{
    public class TubesContainer : MonoBehaviour
    {
        public IObjectPool<TubesContainer> pool;

        [SerializeField] private TubesContainerPool.ContainerType _currentType;
        public TubesContainerPool.ContainerType CurrentType => _currentType;

        [SerializeField] private List<FireTube> _fireTubes;

        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;

        public List<FireTube> FireTubes => _fireTubes;

        public void EnableGravity()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
        }

        public void DisableGravity()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _collider.enabled = false;
        }

        public void OnTubesEmpty()
        {
            pool.Release(this);
        }
    }
}