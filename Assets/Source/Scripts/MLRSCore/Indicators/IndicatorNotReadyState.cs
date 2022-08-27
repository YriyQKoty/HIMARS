using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class IndicatorNotReadyState : IndicatorBaseState
    {
        public override void Enter(IndicatorStateManager stateManager)
        {
            _ammoLightIndicator.Enable();
        }

        public override void Update(IndicatorStateManager stateManager)
        {
            if (_fireController.IsEmpty)
            {
                _ammoLightIndicator.Disable();
                stateManager.SwitchState(stateManager.IndicatorEmptyAmmoState);
            }
            else
            {
                if ((_launcherRotator.InDeadZone || _launcherRotator.RotationInAction) || _fireController.IsInDelay) return;
                
                _ammoLightIndicator.Disable();
                stateManager.SwitchState(stateManager.IndicatorReadyState);
            }
            
        }

        public IndicatorNotReadyState(AmmoLightIndicator ammoLightIndicator, LauncherRotator launcherRotator, FireController fireController) 
            : base(ammoLightIndicator, launcherRotator, fireController)
        {
        }
    }
}