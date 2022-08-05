using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.LauncherCore
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private List<FireTube> _tubes;
        
        public bool IsInDelay { get; private set; }

        public void Fire()
        {
            if (IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return;
            }
            
            var tube = _tubes.FirstOrDefault(t => t.IsReady);

            if (tube != null)
            {
                tube.Fire();

                IsInDelay = true;

                DOVirtual.DelayedCall(tube.Delay, () => IsInDelay = false);
            }
            else
            {
                Debug.LogError("All tubes are empty!");
            }
          
        }
    }
}