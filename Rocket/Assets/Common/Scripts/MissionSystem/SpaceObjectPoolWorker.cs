using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectPoolWorker: IPoolWorker
    {
        private readonly ObjectPoolStorage _objectPoolStorage;

        public  SpaceObjectPoolWorker(ObjectPoolStorage objectPoolStorage)
        {
            _objectPoolStorage = objectPoolStorage;
        }
               
        public GameObject PopFromPool (ISpawnPosition spawnPosition,GameObject prefab)
        {
            var objectPool = _objectPoolStorage.GetPool(prefab);
            GameObject pooledObject = objectPool.Pop(objectPool.Root.position);
            pooledObject.transform.position = spawnPosition.GetSpawnPosition(prefab.GetComponent<Collider>());
            return pooledObject;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPoolStorage.GetPool(gameObject).Push(gameObject);
        }

    }
}