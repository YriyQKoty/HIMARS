using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class ExitableTrigger : MonoBehaviour
    {
        [SerializeField] private ExitablePartController _exitablePartController;

        private bool _entering = false;
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _exitablePartController.Outline.eraseRenderer = !CamerasManager.Instance.IsPointingAtOutlinedObject();
         
            if (!_exitablePartController.Entered)
            {
                if (_entering ) return;

                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    _exitablePartController.Enter(other.transform);
                }
            }
            else
            {
                if (_entering ) return;
                
                if (Input.GetKeyDown(KeyCode.E) && CamerasManager.Instance.IsPointingAtOutlinedObject())
                {
                    StartCoroutine(Wait());
                    
                    _exitablePartController.Exit(other.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _exitablePartController.Outline.eraseRenderer = true;
        }

        private IEnumerator Wait()
        {
            _entering = true;
            
            yield return new WaitForEndOfFrame();

            _entering = false;
        }

    }
}