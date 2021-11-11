using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects.Asteroid;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class ObjectsProvider
    {
        private readonly AssetProvider _assetProvider;
        private readonly RandomAsteroidProvider _asteroidProvider;
        private readonly RandomBoosterProvider _boosterProvider;

        public ObjectsProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _asteroidProvider = new RandomAsteroidProvider(assetProvider);
            _boosterProvider = new RandomBoosterProvider(assetProvider);
        }

        public GameObject GetRandomSpaceObject()
        {
            int randomValue = Random.Range(0, 500);
            if (randomValue <= 490)
            {
                return _asteroidProvider.GetRandomAsteroid();
            }
            return _boosterProvider.GetRandomBooster();
        }

        public GameObject GetCoin()
        {
            return _assetProvider.Load(AssetPath.DogCoin);
        }
    }
}