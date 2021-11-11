using Common.Scripts.Infrastructure;
using UnityEngine;
using Random = System.Random;

namespace Common.Scripts.MissionSystem
{
    public class AssetProvider
    {
        private Random _random = new Random();

        public GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }

    }
}