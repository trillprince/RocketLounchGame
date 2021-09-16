using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISatelliteSpawner
    {
        public Vector3 LastSpawnPos { get;set; }
        GameObject Spawn();
        void Dispose(GameObject gameObject);
    }
}