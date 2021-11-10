using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class ObjectsForSpawn
    {
        private readonly AssetProvider _assetProvider;

        public ObjectsForSpawn(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject GetRandomObject()
        {
            var randomValue = Random.Range(0, 101);
            if (randomValue > 98)
            {
                return _assetProvider.LoadCollectable();
            }
            return _assetProvider.LoadRandomAsteroid();
        }

        public GameObject GetCoin()
        {
            return _assetProvider.LoadCoin();
        }
    }
}