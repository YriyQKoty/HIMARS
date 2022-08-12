using DefaultNamespace.LauncherCore;

namespace DefaultNamespace.Commands
{
    public class LauncherStopRotateCommand : ICommand
    {
        private LauncherHorizontalRotator _horizontalRotator;
        private LauncherVertRotator _vertRotator;
        
        public LauncherStopRotateCommand(LauncherHorizontalRotator horizontalRotator, LauncherVertRotator vertRotator)
        {
            _horizontalRotator = horizontalRotator;
            _vertRotator = vertRotator;
        }
        public void Execute()
        {
            _horizontalRotator.Stop();
            _horizontalRotator.Source.Stop();
            _vertRotator.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}