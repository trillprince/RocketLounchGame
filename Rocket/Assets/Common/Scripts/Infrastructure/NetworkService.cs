using System;
using Common.Scripts.Firebase;

namespace Common.Scripts.Infrastructure
{
    public class NetworkService
    {
        private FirebaseBootStrap _firebase;

        public NetworkService(Action onFirebaseInit)
        {
            _firebase = new FirebaseBootStrap(onFirebaseInit);
        }
    }
}