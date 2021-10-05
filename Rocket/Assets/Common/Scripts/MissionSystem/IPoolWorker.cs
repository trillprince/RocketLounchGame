using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface IPoolWorker
    {
        GameObject PopFromPool(ISpawnPosition spawnPosition);
        void Dispose(GameObject gameObject);
    }
}