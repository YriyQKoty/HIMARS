using System;
using cakeslice;
using DefaultNamespace;
using Scriptables.Cameras;
using UnityEngine;

namespace Cameras
{
    public class OutlineCamera : MonoBehaviour
    {
        [Header("Camera & Outline")] [Space]
        [SerializeField] private Camera _camera;
        [SerializeField] private OutlineEffect _outlineEffect;
        [SerializeField] private FPSController _fpsController;

        [Header("Scriptable data")] [Space]
        [SerializeField] private OutlineCameraData _data;

        private Quaternion _cameraInitRot;

        public Camera Camera => _camera;

        public OutlineEffect OutlineEffect => _outlineEffect;

        private void Start()
        {
            _cameraInitRot = _camera.transform.rotation;
        }

        public void OnDisable()
        {
            if (_outlineEffect != null)
            {
                Destroy(_outlineEffect);
                _outlineEffect = null;
            }

            _fpsController.enabled = false;
            _camera.enabled = false;
            _camera.transform.rotation = _cameraInitRot;

        }

        private void OnEnable()
        {
            _camera.enabled = true;
            _fpsController.enabled = true;
            _outlineEffect = gameObject.AddComponent<OutlineEffect>();
            
            SetOutlineEffectData();
        }

        private void SetOutlineEffectData()
        {
            _outlineEffect.lineThickness = _data.LineThickness;
            _outlineEffect.lineIntensity = _data.LineIntensity;


            _outlineEffect.lineColor0 = _data.LineColor0;
            _outlineEffect.lineColor1 = _data.LineColor1;
            _outlineEffect.lineColor2 = _data.LineColor2;
            
            _outlineEffect.fillAmount = _data.FillAmount;
            _outlineEffect.fillColor = _data.FillColor;
            _outlineEffect.useFillColor = _data.UseFillColor;

            _outlineEffect.backfaceCulling = _data.BackfaceCulling;
            _outlineEffect.additiveRendering = _data.AdditiveRendering;
        }
    }
}