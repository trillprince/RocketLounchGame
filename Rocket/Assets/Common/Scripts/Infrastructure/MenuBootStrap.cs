using System;
using Common.Scripts.Data;
using Common.Scripts.Firebase;
using Firebase.Auth;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrap
    {
        private readonly FirebaseBootStrap _firebaseBootStrap;
        private readonly Authentication _authentication;

        public MenuBootStrap(FirebaseBootStrap firebaseBootStrap, Authentication authentication)
        {
            _firebaseBootStrap = firebaseBootStrap;
            _authentication = authentication;
            _firebaseBootStrap.OnInit += Auth;
            _firebaseBootStrap.Init();
        }

        private void Auth()
        {
            _authentication.ConfigurePlayGames();
        }
    }
}