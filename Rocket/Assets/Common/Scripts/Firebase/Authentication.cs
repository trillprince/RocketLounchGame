using System;
using Common.Scripts.Infrastructure;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Common.Scripts.Firebase
{

    public class Authentication
    {
        public Authentication(Action OnAuth)
        {
            ConfigurePlayGames(OnAuth);
        }
        
        void ConfigurePlayGames(Action onAuth)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestServerAuthCode(false)
                .Build();
            
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.DebugLogEnabled = true;
            Social.localUser.Authenticate((success) => {
                if (success)
                {
                    Debug.Log("ConfigurePlayGames");
                    var authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                    AuthenticatePlayer(authCode,onAuth);
                }
                
            });
        }
        
        void AuthenticatePlayer(string authCode, Action onAuth)
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
                Debug.Log("AuthenticatePlayer");
                onAuth?.Invoke();
            });
        }
        
    }
}