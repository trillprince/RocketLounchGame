using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectPoolWorker: IPoolWorker
    {
        private readonly ObjectPool _objectPool;
        private Vector2 _screenBounds;
        private Vector3 _rocketPosition;

        public  SpaceObjectPoolWorker(RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage,GameObject prefab)
        {
            _objectPool = objectPoolStorage.GetPool(prefab);
            _rocketPosition = rocketMovementController.transform.position;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketMovementController.Rigidbody.position.z));
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