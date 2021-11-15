using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RandomBoosterProvider
    {
        private readonly AssetProvider _assetProvider;

        public RandomBoosterProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        private GameObject GetBlueShield()
        {
            return Load(AssetPath.BlueShield);
        }

        private GameObject GetHologram()
        {
            return Load(AssetPath.Hologram);
        }

        private GameObject Load(string path)
        {
            return  _assetProvider.Load(path);
        }

        public GameObject GetRandomBooster()
        {
            var range = Random.Range(0, 10);
            // if (range <= 4)
            // {
            //     return GetBlueShield();
            // }
            return GetHologram();
        }
    }
}