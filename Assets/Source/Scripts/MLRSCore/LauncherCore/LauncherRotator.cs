using System;
using System.Collections;
using DG.Tweening;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.MLRSCore.LauncherCore
{
    public class LauncherRotator : MonoBehaviour
    {
        [Header("Rotator transforms")] [Space] [SerializeField]
        private Transform _horTransform;

        [SerializeField] private Transform _vertTransform;

        public Transform HorizontalRotTransform => _horTransform;
        public Transform VerticalRotTransform => _vertTransform;

        [Header("Rotator params")] [Space] [SerializeField]
        private Vector2 yAngleRange = new Vector2(-180, 180);

        [SerializeField] private Vector2 xAngleRange = new Vector2(-2, 60);

        [SerializeField] private Vector2 yDeadZoneRange = new Vector2(-10, 10);
        [SerializeField] private Vector2 xDeadZoneRange = new Vector2(-2, 10);

        [Space] [SerializeField] private float rotTime;

        [Header("Audio")] [Space] [SerializeField]
        private AudioSource _source;

        public AudioSource Source => _source;

        private Tween _tweenHor;
        private Tween _tweenVert;

        public Vector2 YAngleRange => yAngleRange;
        public Vector2 XAngleRange => xAngleRange;

        public float CurrentXAngle => _vertTransform.localRotation.eulerAngles.x;
        public float CurrentYAngle => _horTransform.localRotation.eulerAngles.y;

        public bool RotationInAction => _horizontalRotationInAction || _verticalRotationInAction;

        public bool InDeadZone { get; private set; }

        private bool _horizontalRotationInAction;
        private bool _verticalRotationInAction;

        private void Start()
        {
            _source.clip = AudioManager.Instance.CurrentMlrsData.PickAimingSound();
            _source.loop = true;
        }

        private void Update()
        {
            var angles = XYAnglesTo180Deg(_vertTransform.localRotation.eulerAngles.x,
                _horTransform.localRotation.eulerAngles.y);

            var xAngle = angles.x;
            var yAngle = angles.y;

            xAngle = Mathf.Clamp(xAngle, xAngleRange.x, xAngleRange.y);
            yAngle = Mathf.Clamp(yAngle, yAngleRange.x, yAngleRange.y);

            _vertTransform.localRotation = Quaternion.Euler(new Vector3(xAngle, 0, 0));
            _horTransform.localRotation = Quaternion.Euler(new Vector3(0, yAngle, 0));

            if (xAngle >= xDeadZoneRange.x && xAngle <= xDeadZoneRange.y &&
                yAngle >= yDeadZoneRange.x && yAngle <= yDeadZoneRange.y)
            {
                InDeadZone = true;
            }
            else
            {
                InDeadZone = false;
            }
        }

        /// <summary>
        /// Sticks XY euler angles to range [-180;180] deg
        /// </summary>
        /// <returns>Returns Vector2 of X and Y axis</returns>
        private Vector2 XYAnglesTo180Deg(float xAngle, float yAngle)
        {
            xAngle = xAngle > 180 ? xAngle - 360 : xAngle;
            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;

            return new Vector2(xAngle, yAngle);
        }

        public void Rotate(Vector3 angleVector)
        {
            _horizontalRotationInAction = true;
            _verticalRotationInAction = true;

            StartCoroutine(PlayAndStopSound());

            var angles = XYAnglesTo180Deg(_vertTransform.localRotation.eulerAngles.x,
                _horTransform.localRotation.eulerAngles.y);

            _tweenHor = Math.Abs(angles.y - angleVector.y) < 2f
                ? _horTransform.DOLocalRotate(new Vector3(0, angleVector.y, 0), rotTime * 0.05f)
                    .OnComplete(() => { _horizontalRotationInAction = false; })
                : _horTransform.DOLocalRotate(new Vector3(0, angleVector.y, 0), rotTime).OnComplete(() =>
                {
                    _horizontalRotationInAction = false;
                });
            _tweenVert = Math.Abs(angles.x - angleVector.x) < 2f
                ? _vertTransform.DOLocalRotate(new Vector3(angleVector.x, 0, 0), rotTime * 0.05f)
                    .OnComplete(() => { _verticalRotationInAction = false; })
                : _vertTransform.DOLocalRotate(new Vector3(angleVector.x, 0, 0), rotTime).OnComplete(() =>
                {
                    _verticalRotationInAction = false;
                });
        }

        public void Stop(float delay = 0.1f)
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                _tweenHor.Kill();
                _tweenVert.Kill();
            }).OnComplete(() =>
            {
                _horizontalRotationInAction = false;
                _verticalRotationInAction = false;
            });
        }

        private IEnumerator PlayAndStopSound()
        {
            Source.Play();

            yield return new WaitUntil(() => !RotationInAction);

            Source.Stop();
        }
    }
}
