using Interfaces;
using UnityEngine;

namespace MLRSCore.Indicators
{
    public class AmmoReloadIndicator : MonoBehaviour, IAmmoIndicator
    {
        [SerializeField] private Light _indicator;

        public bool IsEnabled => _indicator.enabled;
        public void Enable()
        {
            _indicator.enabled = true;
        }

        public void Disable()
        {
            _indicator.enabled = false;
        }
    }
}