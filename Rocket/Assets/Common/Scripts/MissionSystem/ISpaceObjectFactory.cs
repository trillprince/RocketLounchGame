using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface ISpaceObjectFactory
    {
        GameObject Spawn(ISpawnPosition spawnPosition);
        void Dispose(GameObject gameObject);
    }
}