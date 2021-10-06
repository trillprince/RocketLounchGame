using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class AssetProvider
    {
        public GameObject LoadAsteroid()
        {
            return Resources.Load<GameObject>(AssetPath.Asteroid);
        }

    }
}