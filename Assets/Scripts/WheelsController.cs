using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> _colliders;
    
    void Start()
    {
        foreach (var col in _colliders)
        {
            col.brakeTorque = float.MaxValue;
        }
    }

  
}
