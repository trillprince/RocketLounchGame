using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectSpawner
    {
        GameObject Spawn(ISpawnPosition spawnPosition);
        void Dispose(GameObject gameObject);
    }
}