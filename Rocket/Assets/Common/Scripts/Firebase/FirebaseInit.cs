using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Common.Scripts.Firebase
{
    public class FirebaseInit : MonoBehaviour
    {
        public delegate void FirebaseInitialization();

        public static event FirebaseInitialization FirebaseInited;
    
        private void Awake()
        {
            FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.Log("Failed to initialize firebase " + task.Exception);
                    return;
                }
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                FirebaseInited?.Invoke();
            });
        
        }
    
    
    }
}
