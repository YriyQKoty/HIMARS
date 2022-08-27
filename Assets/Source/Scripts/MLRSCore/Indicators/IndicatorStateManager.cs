using System;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class IndicatorStateManager : MonoBehaviour
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

        [Header("Components")] 
        [SerializeField] private LauncherRotator _launcherRotator;
        [SerializeField] private FireController _fireController;

        private IndicatorBaseState _currentState;

        public IndicatorReadyState IndicatorReadyState { get; private set; }
        public IndicatorNotReadyState IndicatorNotReadyState { get; private set; }
        public IndicatorEmptyAmmoState IndicatorEmptyAmmoState { get; private set; }
        public IndicatorReloadingState IndicatorReloadingState { get; private set; }

        private void Init()
        {
            IndicatorReadyState = new IndicatorReadyState(_ammoReadyIndicator, _launcherRotator, _fireController);
            IndicatorNotReadyState = new IndicatorNotReadyState(_ammoNotReadyIndicator, _launcherRotator, _fireController);
            IndicatorEmptyAmmoState = new IndicatorEmptyAmmoState(_ammoEmptyIndicator, _launcherRotator, _fireController);
            IndicatorReloadingState = new IndicatorReloadingState(_ammoReloadIndicator, _launcherRotator, _fireController);
        }

        private void Start()
        {
            Init();

            _currentState = IndicatorNotReadyState;
            _currentState.Enter(this);
        }

        private void Update()
        {
            _currentState.Update(this);
        }

        public void SwitchState(IndicatorBaseState state)
        {
            _currentState = state;
            _currentState.Enter(this);
        }
    }
}