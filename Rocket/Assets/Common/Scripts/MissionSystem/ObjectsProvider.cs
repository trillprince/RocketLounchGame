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

        public ObjectsProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _asteroidProvider = new RandomAsteroidProvider(assetProvider);
        }

        public GameObject GetRandomSpaceObject()
        {
            int randomValue = Random.Range(0, 500);
            if (randomValue <= 490)
            {
                return _asteroidProvider.GetRandomAsteroid();
            }
            return _assetProvider.Load(AssetPath.Collectable);
        }

        public GameObject GetCoin()
        {
            return _assetProvider.Load(AssetPath.DogCoin);
        }
    }
}