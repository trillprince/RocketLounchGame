using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSatelliteSpawner : ISatelliteSpawner
    {
        private ObjectPool _objectPool;
        private Vector2 _screenBounds;
        private Transform _rocketTransform;
        private GameObject _prefab;
        
        public RightSatelliteSpawner(GameObject prefab, RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage)
        {
            _rocketTransform = rocketMovementController.transform;
            _prefab = prefab;
            _objectPool = objectPoolStorage.GetPool(_prefab);
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketMovementController.Rigidbody.position.z));
        }

        public GameObject Spawn()
        {
            GameObject satellite = _objectPool.Pop(_objectPool.Root.position);
            satellite.transform.position = GetSpawnPosition(satellite.GetComponent<MeshCollider>());
            return satellite;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPool.Push(gameObject);
        }

        private Vector3 GetSpawnPosition(MeshCollider meshCollider)
        {
            return new Vector3(
                (- _screenBounds.x + _rocketTransform.position.x) / 2,
                -_screenBounds.y + meshCollider.bounds.size.y / 2,
                _rocketTransform.position.z);
        }
    }
}