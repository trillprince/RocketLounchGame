using System;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Common.Scripts.Firebase
{

    public class Authentication : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            FirebaseInit.FirebaseInited += ConfigurePlayGames;
        }

        private void OnDisable()
        {
            FirebaseInit.FirebaseInited -= ConfigurePlayGames;
        }
        
        void ConfigurePlayGames()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestServerAuthCode(false)
                .Build();
            
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.DebugLogEnabled = true;
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    var authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                    AuthenticatePlayer(authCode);
                }
                
            });
        }
        
        void AuthenticatePlayer(string authCode)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            Credential credential =
                PlayGamesAuthProvider.GetCredential(authCode);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });
        }
        
    }
}