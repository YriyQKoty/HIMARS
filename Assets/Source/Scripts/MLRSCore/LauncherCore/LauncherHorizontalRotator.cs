using System;
using DG.Tweening;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class LauncherHorizontalRotator : MonoBehaviour, IRotatable
    {
        [Header("Rotator params")]
        [SerializeField] private Vector2 yAngleRange = new Vector2(-140, 140);
        
        [SerializeField] private Vector2 deadZoneRange = new Vector2(-10, 10);
        
        [SerializeField] private float rotTime;

        public Vector2 YAngleRange => yAngleRange;

        private Tween _tween;

        public bool RotationInAction { get; set; }
        
        public bool InDeadZone { get; private set; }

        private void Update()
        {
            var yAngle = transform.localRotation.eulerAngles.y;

            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;

            yAngle = Mathf.Clamp(yAngle, yAngleRange.x, yAngleRange.y);
            
            transform.localRotation = Quaternion.Euler(new Vector3(0,yAngle, 0));
            
            if (yAngle >= deadZoneRange.x && yAngle <= deadZoneRange.y)
            {
                InDeadZone = true;
            }
            else
            {
                InDeadZone = false;
            }
        }

        public Tween Rotate(Vector3 angleVector)
        {
            RotationInAction = true;

            _tween = Math.Abs(transform.localRotation.eulerAngles.y - angleVector.y) < 2f ? 
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
}