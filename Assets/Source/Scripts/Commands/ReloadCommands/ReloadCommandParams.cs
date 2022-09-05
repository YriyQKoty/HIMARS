using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;

namespace Source.Scripts.Commands.ReloadCommands
{
    public interface IReloadCommandParams
    {
        //Receiver
        AmmoReloader AmmoReloader { get; }
        //Receiver
        LauncherRotator LauncherRotator { get; }
        
        //Params
        bool IsInDelay { get; }
        bool IsEmpty { get; }
    }

    public class ReloadCommandParams : IReloadCommandParams
    {
        private readonly LauncherRotator _launcherRotator;
        private readonly AmmoReloader _ammoReloader;
        private readonly FireController _fireController;
        
        public ReloadCommandParams(AmmoReloader ammoReloader, 
            LauncherRotator launcherRotator, 
           FireController fireController)
        {
            _launcherRotator = launcherRotator;
            _ammoReloader = ammoReloader;
            _fireController = fireController;
        }
        
        public AmmoReloader AmmoReloader => _ammoReloader;

        public LauncherRotator LauncherRotator => _launcherRotator;
        
        public bool IsInDelay => _fireController.IsInDelay;
        
        public bool IsEmpty => _fireController.IsEmpty;


    }
}