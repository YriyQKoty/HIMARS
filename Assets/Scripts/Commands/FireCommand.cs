using DefaultNamespace.LauncherCore;
using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class FireCommand : ICommand
    {
        private readonly FireController _fireController;
        private Vector3 _target = Vector3.zero;
        
        public FireCommand(FireController fireController)
        {
            _fireController = fireController;
        }
        
        public void Execute()
        {
            _fireController.Fire(_target);
        }
    }
}