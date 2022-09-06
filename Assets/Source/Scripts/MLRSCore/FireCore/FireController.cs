using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Scripts.Scriptables.Missiles;
using UnityEngine;
using Zenject;

namespace Source.Scripts.MLRSCore.FireCore
{
    public struct FireData
    {
        public Vector3 Target { get; set; }
        public float Angle { get; set; }
        public FireData(Vector3 target, float angle)
        {
            Target = target;
            Angle = angle;
        }
    }
    public class FireController : MonoBehaviour
    {
        private TubesContainer _tubesContainer;
        private FireButton _fireButton;

        private List<FireTube> _tubes;
        
        public event Action OnFireStart;
        public event Action OnFireEnd;
        
        public bool IsInDelay { get; private set; }
        public bool IsEmpty => _tubes.FirstOrDefault(t => t.IsReady) == null;

        public float Delay => _tubes[0].Delay;

        public Missile CurrentMissileCharacteristics => _tubes[0]?.MissileController.Missile;

        public int ReadyTubesCount => _tubes.Where(t => t.IsReady).ToArray().Length;


        [Inject]
        public void Construct(TubesContainer tubesContainer, FireButton fireButton)
        {
            _tubesContainer = tubesContainer;
            _fireButton = fireButton;
            
            _tubes = _tubesContainer.FireTubes;
        }

        public void Fire(FireData data)
        {
            var tube = _tubes.FirstOrDefault(t => t.IsReady);

            if (tube != null)
            {
                OnFireStart?.Invoke();
                
                IsInDelay = true;
                _fireButton.Effect(Delay);

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