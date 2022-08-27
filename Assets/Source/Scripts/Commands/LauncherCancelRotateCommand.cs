using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.Commands
{
    public class LauncherCancelRotateCommand : ICommand
    {
        private readonly LauncherRotator _launcherRotator;
        
        public LauncherCancelRotateCommand(LauncherRotator launcherRotator)
        {
            _launcherRotator = launcherRotator;
        }
        public void Execute()
        {
            _launcherRotator.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}