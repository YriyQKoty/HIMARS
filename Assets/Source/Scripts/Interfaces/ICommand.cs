namespace Source.Scripts.Interfaces
{
    public interface ICommand
    {
        void Execute();

        bool CanExecute();
    }
}