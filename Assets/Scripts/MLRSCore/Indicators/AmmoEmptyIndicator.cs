using Interfaces;
using UnityEngine;

namespace MLRSCore.Indicators
{
    public class AmmoEmptyIndicator : MonoBehaviour, IAmmoIndicator
    {
        [SerializeField] private Animator _indicatorFlashing;
        //[SerializeField] private Light _indicator;

        public bool IsEnabled => _indicatorFlashing.enabled ; 

        public void Enable()
        {
            _indicatorFlashing.enabled = true;
            //_indicator.enabled = true;
        }

        public void Disable()
        {
            _indicatorFlashing.enabled = false;
            //_indicator.enabled = false;
        }
    }
}

