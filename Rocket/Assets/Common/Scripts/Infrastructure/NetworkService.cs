using System;
using Common.Scripts.Firebase;

namespace Common.Scripts.Infrastructure
{
    public class NetworkService
    {
        private FirebaseBootStrap _firebase;

        public static event Action OnFireBaseInit; 

        public NetworkService()
        {
            _firebase = new FirebaseBootStrap(OnFireBaseInit);
        }
    }
}