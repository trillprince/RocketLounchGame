using System;
using Common.Scripts.Data;
using Common.Scripts.Firebase;
using Firebase.Auth;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrap
    {
        private NetworkService _networkService;
        public MenuBootStrap()
        {
            _networkService = new NetworkService();
        }
    }
}