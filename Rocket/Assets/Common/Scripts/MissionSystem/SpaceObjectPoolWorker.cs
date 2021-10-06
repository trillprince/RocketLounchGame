using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectPoolWorker: IPoolWorker
    {
        private readonly ObjectPool _objectPool;

        public  SpaceObjectPoolWorker(RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage,GameObject prefab)
        {
            _objectPool = objectPoolStorage.GetPool(prefab);
        }
               
        public GameObject PopFromPool (ISpawnPosition spawnPosition)
        {
            GameObject pooledObject = _objectPool.Pop(_objectPool.Root.position);
            pooledObject.transform.position = spawnPosition.GetSpawnPosition();
            return pooledObject;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPool.Push(gameObject);
        }

    }
}