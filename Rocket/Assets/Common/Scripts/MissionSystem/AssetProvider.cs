using Common.Scripts.Infrastructure;
using UnityEngine;
using Random = System.Random;

namespace Common.Scripts.MissionSystem
{
    public class AssetProvider
    {
        private Random _random = new Random();
        public GameObject LoadRandomAsteroid()
        {
            var randomValue = _random.Next(0, 100);
            if (randomValue <= 20)
            {
                return Load(AssetPath.Asteroid1);
                
            }
            if (randomValue <= 40)
            {
                return Load(AssetPath.Asteroid2);
            }
            if (randomValue <= 60)
            {
                return Load(AssetPath.Asteroid3);
            }
            return Load(AssetPath.Asteroid4);
        }

        public GameObject LoadCargo()
        {
            return Load(AssetPath.Cargo);
        }

        private GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }

        public GameObject LoadCollectable()
        {
            return Load(AssetPath.Collectable);
        }
    }
}