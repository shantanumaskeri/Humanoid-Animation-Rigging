using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Input.Game
{
    public class GameInputController : MonoBehaviour
    {
        public GameObject playerRig;
        public GameObject playerFbx;
        public GameObject playerRagdoll;

        private bool _isDead;
        
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (_isDead)
                return;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                ActivateRagdoll();
        }

        private void ActivateRagdoll()
        {
            playerRig.SetActive(false);
            playerFbx.SetActive(false);
            playerRagdoll.SetActive(true);

            _isDead = true;
        }
    }
}
