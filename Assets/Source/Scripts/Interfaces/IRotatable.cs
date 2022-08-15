
using DG.Tweening;
using UnityEngine;

namespace Interfaces
{
    public interface IRotatable
    {
        Tween Rotate(Vector3 angleVector);
    }
}