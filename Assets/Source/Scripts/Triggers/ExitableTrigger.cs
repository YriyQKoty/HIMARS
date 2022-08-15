using System.Collections;
using Source.Scripts.Camera;
using Source.Scripts.PlayerLogic;
using UnityEngine;

namespace Source.Scripts.Triggers
{
    public class ExitableTrigger : MonoBehaviour
    {
        [SerializeField] private ExitablePartController _exitablePartController;
        
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
            
            _exitablePartController.Outline.eraseRenderer = !CamerasManager.Instance.IsPointingAtOutlinedObject();

            if (!_exitablePartController.Entered)
            {
                if (_entering ) return;

                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    _exitablePartController.Enter(_player.transform);
                }
            }
            else
            {
                if (_entering ) return;
                
                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    _exitablePartController.Exit(_player.transform);
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