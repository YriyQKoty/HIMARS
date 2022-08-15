using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.MLRSCore.FireCore
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private List<FireTube> _tubes;

        public event Action OnFireStart;
        public event Action OnFireEnd;
        
        public bool IsInDelay { get; private set; }
        public bool IsEmpty => _tubes.FirstOrDefault(t => t.IsReady) == null;

        public float Delay => _tubes[0].Delay;

        public int ReadyTubesCount => _tubes.Where(t => t.IsReady).ToArray().Length;

        public void Fire(Vector3 target)
        {
            var tube = _tubes.FirstOrDefault(t => t.IsReady);

            if (tube != null)
            {
                OnFireStart?.Invoke();
                
                IsInDelay = true;

                DOVirtual.DelayedCall(tube.Delay * 0.1f, () =>
                {
                    tube.Fire(target);
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