namespace DefaultNamespace.Commands
{
    public interface ICommand
    {
        void Execute();

        bool CanExecute();
    }
}