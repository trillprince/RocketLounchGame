using Common.Scripts.Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Scripts.Infrastructure
{
    public class SceneController : MonoBehaviour
    {
        private void OnEnable()
        {
            FirebaseInit.FirebaseInited += LoadLevel;
        }

        private void OnDisable()
        {
            FirebaseInit.FirebaseInited -= LoadLevel;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(2);
        }
    
    }
}
