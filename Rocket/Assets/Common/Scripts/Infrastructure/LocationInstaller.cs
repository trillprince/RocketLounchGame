using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class LocationInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _rocketPrefab;
        public override void InstallBindings()
        {
            BindRocket();
        }

        private void BindRocket()
        {
            Container
                .Bind(typeof(MovementTypeSwitcher),typeof(RocketHeight),typeof(RocketCargo))
                .FromComponentInNewPrefab(_rocketPrefab)
                .AsSingle();
        }

    }
}