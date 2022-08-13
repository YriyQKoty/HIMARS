using MLRSCore.LauncherCore;

namespace Commands
{
    public class LauncherStopRotateCommand : ICommand
    {
        
        private readonly LauncherController _launcherController;
        
        public LauncherStopRotateCommand(LauncherController launcherController)
        {
            _launcherController = launcherController;
        }
        public void Execute()
        {
            if (!_launcherController.FireController.IsEmpty)
            {
                if (_launcherController.InDeadZone)
                {
                    _launcherController.IndicatorsController.NotReady();
                }
                else
                {
                    _launcherController.IndicatorsController.Ready();
                }
               
            }
            else
            {
                _launcherController.IndicatorsController.Empty();
            }
          

            _launcherController.HorizontalRotator.Stop();
            _launcherController.HorizontalRotator.Source.Stop();
            _launcherController.VertRotator.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}