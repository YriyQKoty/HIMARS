using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.LauncherCore
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private List<FireTube> _tubes;

        public event Action OnFireStart;
        public event Action OnFireEnd;
        
        public bool IsInDelay { get; private set; }

        public void Fire(Vector3 target)
        {
            if (IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return;
            }
            
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
                    IsInDelay = false;
                    OnFireEnd?.Invoke();
                });
            }
            else
            {
                Debug.LogError("All tubes are empty!");
            }
          
        }
    }
}