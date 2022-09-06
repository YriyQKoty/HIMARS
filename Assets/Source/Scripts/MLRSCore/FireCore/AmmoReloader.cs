using System.Runtime.InteropServices;
using DG.Tweening;
using Source.Scripts.Helpers;
using UnityEngine;
using Zenject;

namespace Source.Scripts.MLRSCore.FireCore
{
    public class AmmoReloader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Header("Lifting stuff")] [Space]
        private TubesContainer _tubesContainer;
        [SerializeField] private Transform _containerParent;
        
        [SerializeField] private Transform _startLiftPoint;
        [SerializeField] private Transform _endLiftPoint;
        [SerializeField] private float _liftTime = 2f;
        
        public bool IsReloading { get; private set; }

        public void StartReloading() => IsReloading = true;

        [Inject]
        public void Construct(TubesContainer tubesContainer)
        {
            _tubesContainer = tubesContainer;
        }

        public void Reload()
        {
            _animator.SetTrigger(AnimationStateNames.OnReloadTrigger);
        }

        public void LiftEmptyContainer()
        {
            _tubesContainer.transform.DOLocalMove(_endLiftPoint.localPosition, _liftTime)
                .OnComplete(() =>
                {
                    _tubesContainer.EnableGravity();
                    _tubesContainer.transform.parent = null;
                 
                    _animator.ResetTrigger(AnimationStateNames.OnReloadTrigger);
                    IsReloading = false;
                    //Invoke(nameof(LiftFullContainer), 0.5f);
                });
        }

        private void LiftFullContainer()
        {
            // _tubesContainer.transform.parent = _containerParent;
            // _tubesContainer.DisableGravity();
            // _tubesContainer.transform.DOLocalMove(_startLiftPoint.localPosition, _liftTime).OnComplete(() =>
            // {
            //    
            // });
        }
    }
}