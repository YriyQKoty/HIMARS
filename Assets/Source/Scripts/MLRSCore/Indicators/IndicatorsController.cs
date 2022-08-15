using UnityEngine;

namespace MLRSCore.Indicators
{
    public class IndicatorsController : MonoBehaviour
    {
        [Header("Indicators")] 
        [Space] 
        [SerializeField] private AmmoReadyIndicator _ammoReadyIndicator;
        [Space]
        [SerializeField] private AmmoNotReadyIndicator ammoNotReadyIndicator;
        [Space]
        [SerializeField] private AmmoEmptyIndicator _ammoEmptyIndicator;
        [Space]
        [SerializeField] private AmmoReloadIndicator _ammoReloadIndicator;

        public void Reloading()
        {
            ammoNotReadyIndicator.Disable();
            _ammoReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            
            _ammoReloadIndicator.Enable();
        }

        public void Ready()
        {
            ammoNotReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            _ammoReadyIndicator.Enable();
        }

        public void NotReady()
        {
            _ammoReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            ammoNotReadyIndicator.Enable();
        }

        public void Empty()
        {
            _ammoReadyIndicator.Disable();
            ammoNotReadyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            _ammoEmptyIndicator.Enable();
        }
    }
}