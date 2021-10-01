using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectFactory: ISpaceObjectFactory
    {
        protected readonly ObjectPool _objectPool;
        protected Vector2 _screenBounds;
        protected Vector3 _rocketPosition;

        public  SpaceObjectFactory(RocketMovementController rocketMovementController,
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
               
        public GameObject Spawn (ISpawnPosition spawnPosition)
        {
            GameObject satellite = _objectPool.Pop(_objectPool.Root.position);
            satellite.transform.position = spawnPosition.GetSpawnPosition(satellite.GetComponent<MeshCollider>());
            return satellite;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPool.Push(gameObject);
        }

    }
}