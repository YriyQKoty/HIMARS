using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class AmmoNotReadyIndicator : MonoBehaviour, IAmmoIndicator
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