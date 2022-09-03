using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.Commands.LauncherCommands
{
    public interface ILauncherRotateCommandParams
    {
        LauncherRotator LauncherRotator { get; }
    }

    public class LauncherRotateCommandParams : ILauncherRotateCommandParams
    {
        private readonly LauncherRotator _launcherRotator;

        public LauncherRotateCommandParams(LauncherRotator launcherRotator)
        {
            _launcherRotator = launcherRotator;
        }

        public LauncherRotator LauncherRotator => _launcherRotator;
    }
}