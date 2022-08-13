using cakeslice;
using DG.Tweening;
using Helpers;
using MLRSCore.Indicators;
using MLRSCore.LauncherCore;
using UnityEngine;

namespace MLRSCore.FireCore
{
    public class FireButton : MonoBehaviour
    {
        [Header("Launcher")][Space]
        [SerializeField] private LauncherController _launcherController;

        [Header("Indicators")] [Space] 
        
        [SerializeField] private IndicatorsController _indicatorsController;
        
        [Header("Animations")][Space]
        [SerializeField] private Animator _animator;

        [Header("Outline script")][Space]
        [SerializeField] private Outline _outline;
        
        public void Fire()
        {
            _launcherController.Fire(() =>
            {
                _animator.SetBool(AnimationStateNames.FirePressed, true);
                _indicatorsController.NotReady();
                
                DOVirtual.DelayedCall(_launcherController.FireController.Delay, () =>
                {
                    _animator.SetBool(AnimationStateNames.FirePressed, false);

                    if (!_launcherController.FireController.IsEmpty)
                    {
                        if (!_launcherController.RotationInAction)
                        {
                            _indicatorsController.Ready();
                        }
                    }
                    else
                    {
                        _indicatorsController.Empty();
                    }
                }, false);
            });
        }
    }
}