using cakeslice;
using Source.Scripts.PlayerLogic;
using Source.Scripts.Scriptables.Camera;
using UnityEngine;

namespace Source.Scripts.Camera
{
    public class OutlineCamera : MonoBehaviour
    {
        [Header("Camera & Outline")] [Space]
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private OutlineEffect _outlineEffect;
        [SerializeField] private FPSController _fpsController;

        [Header("Scriptable data")] [Space]
        [SerializeField] private OutlineCameraData _data;

        private Quaternion _cameraInitRot;

        public UnityEngine.Camera Camera => _camera;

        private void Start()
        {
            _cameraInitRot = _camera.transform.localRotation;
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
            _camera.transform.localRotation = _cameraInitRot;

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