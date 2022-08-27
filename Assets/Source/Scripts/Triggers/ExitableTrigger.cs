using System.Collections;
using Source.Scripts.Camera;
using Source.Scripts.PlayerLogic;
using UnityEngine;

namespace Source.Scripts.Triggers
{
    public class ExitableTrigger : MonoBehaviour
    {
        [SerializeField] private SeatController seatController;
        
        private bool _entering = false;

        private PlayerController _player;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            if (_player == null)   _player = other.GetComponent<PlayerController>();
          
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _player = null;
        }

        private void Update()
        {
            if (_player == null) return;
            
            seatController.Outline.eraseRenderer = !CamerasManager.Instance.IsPointingAtOutlinedObject();

            if (!seatController.Entered)
            {
                if (_entering ) return;

                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    seatController.Enter(_player.transform);
                }
            }
            else
            {
                if (_entering ) return;
                
                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    seatController.Exit(_player.transform);
                }
            }
        }

        private IEnumerator Wait()
        {
            _entering = true;
            
            yield return new WaitForEndOfFrame();

            _entering = false;
        }

    }
}