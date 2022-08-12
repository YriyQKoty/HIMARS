
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IRotatable
    {
        Tween Rotate(Vector3 angleVector);
    }
}