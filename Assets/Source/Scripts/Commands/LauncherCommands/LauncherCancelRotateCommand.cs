using Source.Scripts.Interfaces;

namespace Source.Scripts.Commands.LauncherCommands
{
    public class LauncherCancelRotateCommand : ICommand
    {
        private readonly ILauncherRotateCommandParams _commandParams;
        
        public LauncherCancelRotateCommand(ILauncherRotateCommandParams commandParams)
        {
            _commandParams = commandParams;
        }
        public void Execute()
        {
            _commandParams.LauncherRotator.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}