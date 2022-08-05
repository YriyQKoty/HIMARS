using DefaultNamespace.LauncherCore;

namespace DefaultNamespace.Commands
{
    public class FireCommand : ICommand
    {
        private readonly FireController _fireController;
        
        public FireCommand(FireController fireController)
        {
            _fireController = fireController;
        }
        
        public void Execute()
        {
            _fireController.Fire();
        }
    }
}