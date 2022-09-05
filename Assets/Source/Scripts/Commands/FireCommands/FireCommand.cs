using Source.Scripts.Interfaces;
using Source.Scripts.MLRSCore.FireCore;
using UnityEngine;

namespace Source.Scripts.Commands.FireCommands
{
    public class FireCommand : ICommand
    {
        private readonly IFireCommandParams _commandParams;

        private FireData _fireData;

        public FireCommand(IFireCommandParams commandParams)
        {
            _commandParams = commandParams;
            _fireData = new FireData(_commandParams.PointOfAimingPosition, _commandParams.VerticalRotTransform, 0);
        }
        
        public void Execute()
        {
            _fireData.Target = _commandParams.PointOfAimingPosition;
            _fireData.Angle = _commandParams.CurrentXAngle;
            
            _commandParams.FireController.Fire(_fireData);
        }

        public bool CanExecute()
        {
            if (_commandParams.RotationInAction)
            {
                Debug.LogWarning("Rotation still in Action! Wait for finishing!");
                return false;
            }

            if ( _commandParams.InDeadZone)
            {
                Debug.LogWarning("Launcher is in Dead zone. Rotate Launcher to fire.");
                return false;
            }

            if (  _commandParams.FireController.IsInDelay)
            {
                Debug.LogWarning("Is in delay. Wait!");
                return false;
            }

            if ( _commandParams.FireController.IsEmpty)
            {
                Debug.LogWarning("All tubes are empty! Cannot fire!");
                return false;
            }

            //if distance to target is greater than maximum range (always when angle is 45 deg)
            if (_commandParams.DistanceToTarget >
                _commandParams.FireController.CurrentMissileCharacteristics.MaximumRange)
            {
                Debug.LogWarning("Unreachable target! Cannot fire!");
                return false;
            }
            
            if (_commandParams.IsReloading) 
            {
                Debug.LogWarning("Reloading still in Action! Wait for finishing!");
                return false;
            }
            
            return true;
        }
    }
}