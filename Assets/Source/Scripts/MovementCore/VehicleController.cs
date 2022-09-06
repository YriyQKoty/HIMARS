using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Commands;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.Triggers;
using UnityEngine;
using Zenject;

namespace Source.Scripts.MovementCore
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        
        [Header("Launcher & Firing components")]
        
        [SerializeField] private List<SeatController> _exitableSeats;

        private SeatController _operatorPart;
        private SeatController _driverPart;

        private CommandsController _commandsController;
        
        private bool IsMoving = false;

        [Inject]
        public void Construct(CommandsController commandsController)
        {
            _commandsController = commandsController;
        }

        private void FixedUpdate()
        {
            if (!IsMoving) return;

            if (_driverPart == null)
            {
                Debug.LogWarning("Driver seat was not set!");
                return;
            }
            
            if (!_driverPart.Entered) return;
         
            _movementController.Move();
        }
        
        

        private void Update()
        {
            if (!IsMoving) return;
            
            if (_operatorPart == null) return;
            
            if (!_operatorPart.Entered) return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                _commandsController.Reload();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _commandsController.RotateToTarget();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                _commandsController.CancelRotation();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                _commandsController.Fire();
            }

        }

        private void Start()
        {
            _operatorPart = _exitableSeats.SingleOrDefault(e => e.Seat == SeatType.Operator);
            _driverPart = _exitableSeats.SingleOrDefault(e => e.Seat == SeatType.Driver);
        }

        private void OnEnable()
        {
            foreach (var seat in _exitableSeats)
            {
                seat.OnEnter += OnVehicleEnter;
                seat.OnExit += OnVehicleExit;
            }
        }

        private void OnDisable()
        {
            foreach (var seat in _exitableSeats)
            {
                seat.OnEnter -= OnVehicleEnter;
                seat.OnExit -= OnVehicleExit;
            }
        }

        private void OnVehicleEnter()
        {
            IsMoving = true;
        }
        
        private void OnVehicleExit()
        {
            IsMoving = false;
        }
    }
}