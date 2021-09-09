using System;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Common.Scripts.Firebase
{
    public class FirebaseBootStrap
    {
        public FirebaseBootStrap(Action onFireBaseInit)
        {
            Init(onFireBaseInit);
        }

        private static void Init(Action onFireBaseInit)
        {
            FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.Log("Failed to initialize firebase " + task.Exception);
                    return;
                }

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                onFireBaseInit?.Invoke();
            });
        }
    }
}
