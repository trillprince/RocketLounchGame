using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteSpawner: ISpawner
    {
        private ObjectPool _objectPool;
        private Vector2 _screenBounds;
        private Transform _rocketTransform;
        private GameObject _prefab;

        public SatelliteSpawner(GameObject prefab, Transform rocketTransform,Rigidbody  rocketRigidbody, ObjectPoolStorage objectPoolStorage)
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
            GameObject satellite =_objectPool.Pop();
            satellite.transform.position = CalculatePosition(satellite.GetComponent<MeshCollider>(),_rocketTransform);
            return satellite;
        }

        public void Dispose(GameObject gameObject)
        {
            _objectPool.Push(gameObject);
        }

        private Vector3 CalculatePosition(MeshCollider meshCollider, Transform transform)
        {
            return new Vector3((_screenBounds.x - transform.position.x)/2,
                _screenBounds.y + meshCollider.bounds.size.y,transform.position.z );
        }
    }
}