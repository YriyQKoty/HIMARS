using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.MLRSCore.Indicators
{
    public class IndicatorEmptyAmmoState : IndicatorBaseState
    {
        public override void Enter(IndicatorStateManager stateManager)
        {
            _ammoLightIndicator.Enable();
        }

        public override void Update(IndicatorStateManager stateManager)
        {
            
        }

        public IndicatorEmptyAmmoState(AmmoLightIndicator ammoLightIndicator, LauncherRotator launcherRotator, FireController fireController) : base(ammoLightIndicator, launcherRotator, fireController)
        {
        }
    }
}