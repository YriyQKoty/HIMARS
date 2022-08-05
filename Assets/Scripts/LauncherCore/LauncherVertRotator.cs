using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class LauncherVertRotator : MonoBehaviour, IRotatable
{
    [SerializeField] private Vector2 xAngleRange = new Vector2(-2, 60);
    [SerializeField] private float rotTime;

    public Vector2 XAngleRange => xAngleRange;

    public bool RotationInAction { get; set; }

    private Tween _tween;

    private void Update()
    {
        var xAngle = transform.localRotation.eulerAngles.x;

        xAngle = xAngle > 180 ? xAngle - 360 : xAngle;

        xAngle = Mathf.Clamp(xAngle, xAngleRange.x, xAngleRange.y);
            
        transform.localRotation = Quaternion.Euler(new Vector3(xAngle,0, 0));
    }

    public Tween Rotate(Vector3 angleVector)
    {
        RotationInAction = true;
        
        _tween = Math.Abs(transform.localRotation.eulerAngles.x - angleVector.x) < 2f ? 
            transform.DOLocalRotate(angleVector, rotTime * 0.05f) : transform.DOLocalRotate(angleVector, rotTime);

        return _tween;
    }

    public void Stop(float delay = 0.1f)
    {
        DOVirtual.DelayedCall(delay, () =>
        {
            _tween.Kill();
        }).OnComplete(() =>
        {
            RotationInAction = false;
        });
    }
}