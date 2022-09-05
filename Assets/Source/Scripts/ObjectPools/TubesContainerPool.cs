using System;
using System.ComponentModel.Design;
using Source.Scripts.Helpers.Pool;
using Source.Scripts.MLRSCore.FireCore;
using UnityEngine;

namespace Source.Scripts.ObjectPools
{
    public class TubesContainerPool : MonoBehaviour
    {
        public enum ContainerType
        {
            _2x3 = default,
            _1x1,
            _1x2
        }
        [Header("Instance")][Space]
        [SerializeField] private TubesContainer _prefab;
        
        [Header("Params")][Space]
        public bool collectionChecks = true;
        public int maxPoolSize = 10;
        [SerializeField] private ContainerType _currentType = ContainerType._2x3;
        
        public ContainerType CurrentType => _currentType;

        IObjectPool<TubesContainer> m_Pool;


        private void ContainerTypeCheck()
        {
            if (_prefab.CurrentType != _currentType)
            {
                throw new Exception("Invalid prefab container type or pool container type! Change prefab or current pool container type!");
            }
        }

        private void OnValidate()
        {
            ContainerTypeCheck();
        }

        public IObjectPool<TubesContainer> Pool
        {
            get
            {
                ContainerTypeCheck();
                
                if (m_Pool == null)
                {
                    m_Pool = new ObjectPool<TubesContainer>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                        OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                }

                return m_Pool;
            }
        }

        TubesContainer CreatePooledItem()
        {
            ContainerTypeCheck();
            
            var tc = Instantiate(_prefab,  Vector3.zero, Quaternion.identity, transform);
            tc.pool = Pool;
            
            return tc;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(TubesContainer container)
        {
            container.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(TubesContainer container)
        {
            container.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(TubesContainer container)
        {
            Destroy(container.gameObject);
        }
        
    }
}