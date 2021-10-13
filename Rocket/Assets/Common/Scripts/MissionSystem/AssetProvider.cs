using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class AssetProvider
    {
        public GameObject LoadAsteroid()
        {
            return Load(AssetPath.Asteroid);
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