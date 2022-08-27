using Source.Scripts.MLRSCore.FireCore;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.RecoilCore
{
    public class RecoilController : MonoBehaviour
    {
        [SerializeField] private Animator _recoilAnimator;
        
        [SerializeField] private FireController _fireController;
        [SerializeField] private LauncherRotator _launcherRotator;

        private readonly string XParameter = "X";
        private readonly string YParameter = "Y";
        private readonly string FireParameter = "Fire";

        private void Update()
        {
            var yAngle = _launcherRotator.CurrentYAngle;

            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;
            
            _recoilAnimator.SetFloat(XParameter, Mathf.Sin(Mathf.Deg2Rad*yAngle));
            _recoilAnimator.SetFloat(YParameter, Mathf.Cos(Mathf.Deg2Rad*yAngle));
        }

        private void OnEnable()
        {
            _fireController.OnFireStart += StartRecoil;
            _fireController.OnFireEnd += EndRecoil;
        }

        private void OnDisable()
        {
            _fireController.OnFireStart -= StartRecoil;
            _fireController.OnFireEnd -= EndRecoil;
        }

        private void StartRecoil()
        {
            _recoilAnimator.SetBool(FireParameter, true);
        }

        private void EndRecoil()
        {
            _recoilAnimator.SetBool(FireParameter, false);
        }
    }
}