using UnityEngine;

namespace DefaultNamespace.MovementCore
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private WheelCollider _collider;
        [SerializeField] private Transform _wheelMesh;

        public WheelCollider Collider => _collider;

        public Transform WheelMesh => _wheelMesh;
        
        public void UpdateWheel()
        {
            Vector3 pos = _wheelMesh.position;
            Quaternion rot = _wheelMesh.rotation;
            
            Collider.GetWorldPose(out pos, out rot);

            _wheelMesh.position = pos;
            _wheelMesh.rotation = rot;
        }
        
    }
}