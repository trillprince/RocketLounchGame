using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSpawner: ISpaceObjectSpawner
    {
        private ObjectPool _objectPool;
        protected Vector2 _screenBounds;
        protected Vector3 _rocketPosition;
        private GameObject _prefab;

        public  SpaceObjectSpawner(GameObject prefab, RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage)
        {
            _rocketPosition = rocketMovementController.transform.position;
            _prefab = prefab;
            _objectPool = objectPoolStorage.GetPool(_prefab);
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketMovementController.Rigidbody.position.z));
        }

        public GameObject Spawn(ISpawnPosition spawnPosition)
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