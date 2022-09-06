using cakeslice;
using DG.Tweening;
using Source.Scripts.Helpers;
using UnityEngine;
using Zenject;


namespace Source.Scripts.MLRSCore.FireCore
{
    public class FireButton : MonoBehaviour
    {
        [Header("Animations")][Space]
        [SerializeField] private Animator _animator;

        [Header("Outline script")][Space]
        [SerializeField] private Outline _outline;

        public void Effect(float time = 1f)
        {
            _animator.SetBool(AnimationStateNames.FirePressed, true);

            DOVirtual.DelayedCall(time, () =>
            {
                _animator.SetBool(AnimationStateNames.FirePressed, false);
            });
        }
    }
}