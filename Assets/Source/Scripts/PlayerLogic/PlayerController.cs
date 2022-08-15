using UnityEngine;

namespace Source.Scripts.PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FPSController _fpsController;
        [SerializeField] private CharacterController _characterController;
        
        private Vector3 _movementDirection;
        private Vector3 v_velocity;
        
        
        [Header("Speed parameters")][Space]
        [SerializeField] private float speed = 6.0f;

        private float deltaX;
        private float deltaZ;
        [SerializeField] private float gravity = 0;
        
        public bool IsInVehicle => !_characterController.enabled;

       [SerializeField] private ViewMode _currentViewMode;

        public enum ViewMode
        {
            FPS,
            Third
        }

        public void SwitchMode(ViewMode viewMode)
        {
            if (viewMode == ViewMode.FPS)
            {
                _fpsController.enabled = true;
                _currentViewMode = ViewMode.FPS;
                Cursor.visible = false;
            }
            else
            {
                _fpsController.enabled = false;
                _currentViewMode = ViewMode.Third;
                Cursor.visible = true;
            }
        }

        private void OnTransformParentChanged()
        {
            _characterController.enabled = !_characterController.enabled;
        }

        private void FixedUpdate()
        {
            if (_currentViewMode == ViewMode.FPS)
            {
                FPSMove();
            }
        }

        private void FPSMove()
        {
            if (IsInVehicle) return;
            
            deltaX = Input.GetAxis("Horizontal")*speed;
            deltaZ = Input.GetAxis("Vertical")*speed;
        
        
            var movement = new Vector3(deltaX,0,deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = 0;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _characterController.Move(movement);

            _characterController.Move(new Vector3(0, gravity * Time.deltaTime, 0));
        }
    }
}