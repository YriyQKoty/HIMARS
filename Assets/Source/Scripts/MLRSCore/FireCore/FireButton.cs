using cakeslice;
using DG.Tweening;
using Source.Scripts.Helpers;
using Source.Scripts.MLRSCore.Indicators;
using Source.Scripts.MLRSCore.LauncherCore;
using UnityEngine;

namespace Source.Scripts.MLRSCore.FireCore
{
    public class FireButton : MonoBehaviour
    {
        [Header("Launcher")][Space]
        [SerializeField] private LauncherController _launcherController;
        
        [Header("Animations")][Space]
        [SerializeField] private Animator _animator;

        [Header("Outline script")][Space]
        [SerializeField] private Outline _outline;
        
        public void Fire()
        {
            _launcherController.Fire(Callback);
        }

        private void Callback()
        {
            _animator.SetBool(AnimationStateNames.FirePressed, true);

            DOVirtual.DelayedCall(_launcherController.FireController.Delay, () =>
            {
                _animator.SetBool(AnimationStateNames.FirePressed, false);
            });
        }
    }
}