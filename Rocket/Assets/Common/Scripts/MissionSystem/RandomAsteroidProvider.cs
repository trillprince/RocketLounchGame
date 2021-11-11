using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RandomAsteroidProvider
    {
        private readonly AssetProvider _assetProvider;
        private float _minAsteroidSize = 0.8f;
        private float _maxAsteroidSize = 1.2f;
        private Vector3 _scaleVec;

        public RandomAsteroidProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject GetRandomAsteroid()
        {
            int randomValue = Random.Range(0, 100);
            GameObject gameObject = _assetProvider.Load(AssetPath.Asteroid1);
            if (randomValue <= 25)
            {
                gameObject = _assetProvider.Load(AssetPath.Asteroid1);
            }

            if (randomValue <= 50)
            {
                gameObject = _assetProvider.Load(AssetPath.Asteroid2);
            }

            if (randomValue <= 75)
            {
                gameObject = _assetProvider.Load(AssetPath.Asteroid3);
            }

            if (randomValue <= 100)
            {
                gameObject = _assetProvider.Load(AssetPath.Asteroid4);
            }
            return SetAsteroidScale(gameObject);
        }

        private GameObject SetAsteroidScale(GameObject gameObject)
        {
            float randomScale = Random.Range(_minAsteroidSize, _maxAsteroidSize);
            _scaleVec.x = randomScale;
            _scaleVec.y = randomScale;
            _scaleVec.z = randomScale;

            gameObject.transform.localScale = _scaleVec;

            return gameObject;
        }
    }
}