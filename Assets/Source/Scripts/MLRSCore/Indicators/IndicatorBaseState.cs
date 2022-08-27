using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.MLRSCore.Indicators
{
    public abstract class IndicatorBaseState
    {
        protected LauncherRotator _launcherRotator;
        protected FireController _fireController;
        protected AmmoLightIndicator _ammoLightIndicator;
        
        public IndicatorBaseState(AmmoLightIndicator ammoLightIndicator, LauncherRotator launcherRotator, FireController fireController)
        {
            _ammoLightIndicator = ammoLightIndicator;
            _launcherRotator = launcherRotator;
            _fireController = fireController;
        }
        public abstract void Enter(IndicatorStateManager stateManager);

        public abstract void Update(IndicatorStateManager stateManager);
        
    }
}