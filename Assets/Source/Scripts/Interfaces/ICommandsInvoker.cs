namespace Source.Scripts.Interfaces
{
    public enum CommandType
    {
        RotateLauncherToDefault = default,
        RotateLauncherToTarget,
        RotateLauncherRandom,
        CancelLauncherRotation,
        Reload,
        FireOne
    }
    public interface ICommandsInvoker
    {
        public void InvokeCommand(CommandType type);
    }
}