using MLRSCore.LauncherCore;
using UnityEngine;

namespace RecoilCore
{
    public class RecoilController : MonoBehaviour
    {
        [SerializeField] private Animator _recoilAnimator;

        [SerializeField] private LauncherController _launcherController;

        private readonly string XParameter = "X";
        private readonly string YParameter = "Y";
        private readonly string FireParameter = "Fire";

        private void Update()
        {
            var yAngle = _launcherController.HorizontalRotator.transform.localRotation.eulerAngles.y;

            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;
            
            _recoilAnimator.SetFloat(XParameter, Mathf.Sin(Mathf.Deg2Rad*yAngle));
            _recoilAnimator.SetFloat(YParameter, Mathf.Cos(Mathf.Deg2Rad*yAngle));
        }

        private void OnEnable()
        {
            _launcherController.FireController.OnFireStart += StartRecoil;
            _launcherController.FireController.OnFireEnd += EndRecoil;
        }

        private void OnDisable()
        {
            _launcherController.FireController.OnFireStart -= StartRecoil;
            _launcherController.FireController.OnFireEnd -= EndRecoil;
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