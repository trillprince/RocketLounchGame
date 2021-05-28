using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class LocationInstaller: MonoInstaller
    {
        [SerializeField] private Transform _rocketSpawnPoint;
        [SerializeField] private GameObject _rocketPrefab;
        public override void InstallBindings()
        {
            BindRocket();
        }

        private void BindRocket()
        {
            RocketMovement rocketMovement =
                Container.InstantiatePrefabForComponent<RocketMovement>(_rocketPrefab, _rocketSpawnPoint.position,
                    Quaternion.identity, null);

            Container
                .Bind<RocketMovement>()
                .FromInstance(rocketMovement)
                .AsSingle()
                .NonLazy();
        }
    }
}