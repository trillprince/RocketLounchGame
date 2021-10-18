using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public interface IPoolWorker
    {
        GameObject PopFromPool(ISpawnPosition spawnPosition,GameObject prefab);
        void Dispose(ISpaceObject spaceObject);
    }
}