using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteSpawner : ISatelliteSpawner
    {
        private ObjectPool _objectPool;
        private Vector2 _screenBounds;
        private Transform _rocketTransform;
        private GameObject _prefab;
        public Vector3 LastSpawnPos { get;set; }

        public SatelliteSpawner(GameObject prefab, Transform rocketTransform, Rigidbody rocketRigidbody,
            ObjectPoolStorage objectPoolStorage)
        {
            _rocketTransform = rocketTransform;
            _prefab = prefab;
            _objectPool = objectPoolStorage.GetPool(_prefab);
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketRigidbody.position.z));
        }

        public GameObject Spawn()
        {
            GameObject satellite = _objectPool.Pop();
            satellite.transform.position = GetSpawnPosition(satellite.GetComponent<MeshCollider>());
            return satellite;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPool.Push(gameObject);
        }

        private Vector3 GetSpawnPosition(MeshCollider meshCollider)
        {
            return LastSpawnPos = new Vector3(
                (_screenBounds.x - _rocketTransform.position.x) / 2,
                -_screenBounds.y + meshCollider.bounds.size.y / 2,
                _rocketTransform.position.z);
        }
    }
}