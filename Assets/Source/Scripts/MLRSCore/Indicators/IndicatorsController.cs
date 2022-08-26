using UnityEngine;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class IndicatorsController : MonoBehaviour
    {
        [Header("Indicators")] 
        [Space] 
        [SerializeField] private AmmoLightIndicator _ammoReadyIndicator;
        [Space]
        [SerializeField] private AmmoLightIndicator _ammoNotReadyIndicator;
        [Space]
        [SerializeField] private AmmoLightIndicator _ammoEmptyIndicator;
        [Space]
        [SerializeField] private AmmoLightIndicator _ammoReloadIndicator;

        public void Reloading()
        {
            _ammoNotReadyIndicator.Disable();
            _ammoReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            
            _ammoReloadIndicator.Enable();
        }

        public void Ready()
        {
            _ammoNotReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            _ammoReadyIndicator.Enable();
        }

        public void NotReady()
        {
            _ammoReadyIndicator.Disable();
            _ammoEmptyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            _ammoNotReadyIndicator.Enable();
        }

        public void Empty()
        {
            _ammoReadyIndicator.Disable();
            _ammoNotReadyIndicator.Disable();
            _ammoReloadIndicator.Disable();
            
            _ammoEmptyIndicator.Enable();
        }
    }
}