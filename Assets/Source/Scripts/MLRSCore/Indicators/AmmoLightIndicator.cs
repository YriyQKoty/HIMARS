using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class AmmoLightIndicator : MonoBehaviour, IAmmoIndicator
    {
        [SerializeField] private Animator _indicatorFlashing;
        [SerializeField] private Light _indicator;
        public bool IsEnabled => _indicator.enabled || _indicatorFlashing.enabled;
        public void Enable()
        {
            if (_indicatorFlashing != null)
            {
                _indicatorFlashing.enabled = true;
            }
            _indicator.enabled = true;
        }

        public void Disable()
        {
            if (_indicatorFlashing != null)
            {
                _indicatorFlashing.enabled = false;
            }
            _indicator.enabled = false;
        }
    }
}