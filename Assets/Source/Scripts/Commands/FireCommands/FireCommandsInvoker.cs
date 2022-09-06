using System;
using Source.Scripts.Interfaces;
using Zenject;

namespace Source.Scripts.Commands.FireCommands
{
    public sealed class FireCommandsInvoker : ICommandsInvoker
    {
        private readonly FireCommand _fireCommand;

        [Inject]
        public FireCommandsInvoker(IFireCommandParams commandParams)
        {
            _fireCommand = new FireCommand(commandParams);
        }
        
        public void InvokeCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.FireOne: Fire();
                    break;
                default:
                    throw new Exception("This invoker cannot execute such a command!");
            }
        }

        private void Fire()
        {
            if (!_fireCommand.CanExecute()) return;
            
            _fireCommand.Execute();
        }
    }
}