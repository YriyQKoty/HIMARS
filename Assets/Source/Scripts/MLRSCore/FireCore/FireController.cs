using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Scripts.Scriptables.Missiles;
using UnityEngine;

namespace Source.Scripts.MLRSCore.FireCore
{
    public struct FireData
    {
        public Vector3 Target { get; set; }
        public float Angle { get; set; }
        public FireData(Vector3 target, Transform spawn, float angle)
        {
            Target = target;
            Angle = angle;
        }
    }
    public class FireController : MonoBehaviour
    {
        [Header("Reloadable Tubes Container")]
        [SerializeField] private TubesContainer _tubesContainer;

        private List<FireTube> _tubes;
        
        public event Action OnFireStart;
        public event Action OnFireEnd;
        
        public bool IsInDelay { get; private set; }
        public bool IsEmpty => _tubes.FirstOrDefault(t => t.IsReady) == null;

        public float Delay => _tubes[0].Delay;

        public Missile CurrentMissileCharacteristics => _tubes[0]?.MissileController.Missile;

        public int ReadyTubesCount => _tubes.Where(t => t.IsReady).ToArray().Length;

        private void Start()
        {
            _tubes = _tubesContainer.FireTubes;
        }

        public void Fire(FireData data)
        {
            var tube = _tubes.FirstOrDefault(t => t.IsReady);

            if (tube != null)
            {
                OnFireStart?.Invoke();
                
                IsInDelay = true;

                DOVirtual.DelayedCall(tube.Delay * 0.1f, () =>
                {
                    tube.Fire(data);
                });
                
                DOVirtual.DelayedCall(tube.Delay, () =>
                {
                    OnFireEnd?.Invoke();
                    
                    IsInDelay = false;
                });
            }
            else
            {
                Debug.LogError("All tubes are empty!");
            }
          
        }
    }
}