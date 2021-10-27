using System;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Common.Scripts.Firebase
{
    public class FirebaseBootStrap
    {
        private static Authentication _auth;

        public FirebaseBootStrap(Action onFireBaseInit)
        {
            Init(onFireBaseInit);
            Debug.Log("firebasebootstrap");
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
                if (task.IsCompleted)
                {
                    Debug.Log("dependencies");
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    _auth = new Authentication((() => { onFireBaseInit?.Invoke(); }));   
                }
            });
        }
    }
}