using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class LocationInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _rocketPrefab;
        [SerializeField] private GameObject [] _backgrounds;
        public override void InstallBindings()
        {
            BindRocket();
        }

        private void BindRocket()
        {
            Container
                .Bind(typeof(OnTouchRocketMove),typeof(RocketHeight))
                .FromComponentInNewPrefab(_rocketPrefab)
                .AsSingle();
        }

    }
}