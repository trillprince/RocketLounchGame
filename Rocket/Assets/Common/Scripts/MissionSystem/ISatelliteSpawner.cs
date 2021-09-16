using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISatelliteSpawner
    {
        GameObject Spawn();
        void Dispose(GameObject gameObject);
    }
}