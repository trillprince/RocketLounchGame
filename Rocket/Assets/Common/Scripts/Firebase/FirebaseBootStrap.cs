using System;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Common.Scripts.Firebase
{
    public class FirebaseBootStrap
    {
        public event Action OnInit;

        public void Init()
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
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    OnInit?.Invoke();
                }
            });
        }
    }
}