using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.Triggers
{
    public class HatchController : ExitablePartController
    {
        [SerializeField] private Vector3 _closedHatch = new Vector3(0, 0, 0);
        [SerializeField] private Vector3 _openedHatch = new Vector3(120f, 0, 0);

        private bool isOpened;

        public void Open()
        {
            if (isOpened) return;
      
            transform.DOLocalRotateQuaternion(Quaternion.Euler(_openedHatch), 1);
      
            isOpened = true;
        }

        public void Close()
        {
            if (!isOpened) return;
      
            transform.DOLocalRotateQuaternion(Quaternion.Euler(_closedHatch), 1);
      
            isOpened = false;
        }

        public override void Enter(Transform player)
        {
            Open();
      
            ChangeCameraOnSeat();
        }

        public override void Exit(Transform player)
        {
            //TODO changing of camera state
            Close();
        }
    }

}