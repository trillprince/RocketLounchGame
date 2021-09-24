using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    internal class MiddleSpaceObjectSpawner : ISpaceObjectSpawner
    {
        private readonly GameObject _prefab;
        private readonly Transform _rocketTransform;
        private readonly ObjectPool _objectPool;
        private readonly Vector3 _screenBounds;

        public MiddleSpaceObjectSpawner(GameObject prefab, RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage)
        {
            _prefab = prefab;
            _rocketTransform = rocketMovementController.GetTransform();
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
                (_rocketTransform.position.x) / 2,
                -_screenBounds.y + meshCollider.bounds.size.y / 2,
                _rocketTransform.position.z);
        }
    }
}