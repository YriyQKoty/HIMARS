using System;
using JetBrains.Annotations;

namespace Source.Scripts.Interfaces
{
    public interface ICommand
    {
        void Execute();

        bool CanExecute();
    }
}