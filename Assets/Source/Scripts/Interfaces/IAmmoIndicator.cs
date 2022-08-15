namespace Source.Scripts.Interfaces
{
    public interface IAmmoIndicator
    {
        bool IsEnabled { get; }
        
        void Enable();

        void Disable();
    }
}