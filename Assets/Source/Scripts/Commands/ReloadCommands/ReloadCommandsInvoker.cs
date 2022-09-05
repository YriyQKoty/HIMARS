using System;
using Source.Scripts.Interfaces;

namespace Source.Scripts.Commands.ReloadCommands
{

    public sealed class ReloadCommandsInvoker : ICommandsInvoker
    {
        private readonly ReloadCommand _reloadCommand;

        public ReloadCommandsInvoker(IReloadCommandParams commandParams)
        {
            _reloadCommand = new ReloadCommand(commandParams);
        }

        public void InvokeCommand(CommandType type)
        {
            switch (type)
            {
                case CommandType.Reload: Reload(); break;
                default:
                    throw new Exception("This invoker cannot execute such a command!");
            }
        }

        private void Reload()
        {
            if (_reloadCommand.CanExecute())
            {
                _reloadCommand.Execute();
            }
        }
    }
}

