using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class IndicatorReadyState : IndicatorBaseState
    {
        
        public override void Enter(IndicatorStateManager stateManager)
        {
            _ammoLightIndicator.Enable();
        }

        public override void Update(IndicatorStateManager stateManager)
        {
            if (!_fireController.IsInDelay && !_launcherRotator.InDeadZone &&
                !_launcherRotator.RotationInAction) return;
            
            _ammoLightIndicator.Disable();
            stateManager.SwitchState(stateManager.IndicatorNotReadyState);
        }

        public IndicatorReadyState(AmmoLightIndicator ammoLightIndicator, LauncherRotator launcherRotator, FireController fireController) : base(ammoLightIndicator, launcherRotator, fireController)
        {
        }
    }
}