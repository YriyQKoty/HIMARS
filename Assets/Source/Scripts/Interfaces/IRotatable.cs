using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IRotatable
    {
        Tween Rotate(Vector3 angleVector);
    }
}