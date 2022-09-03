using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using Source.Scripts.Triggers;
using UnityEngine;

namespace Source.Scripts.MovementCore
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        
        [Header("Launcher & Firing components")]
        [SerializeField] private LauncherController _launcherController;
        [SerializeField] private FireButton _fireButton;
        
        [SerializeField] private List<SeatController> _exitableSeats;

        private SeatController _operatorPart;
        private SeatController _driverPart;

        private bool IsMoving = false;

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
                _launcherController.RotateToTarget();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _launcherController.RotateToDefault();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                _launcherController.CancelRotation();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                _fireButton.Fire();
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