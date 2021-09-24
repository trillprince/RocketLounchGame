using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    internal class MiddleSpaceObjectSpawner : ISpaceObjectSpawner
    {
        private readonly GameObject _prefab;
        private readonly RocketMovementController _rocketMovementController;
        private readonly ObjectPoolStorage _objectPoolStorage;

        public MiddleSpaceObjectSpawner(GameObject prefab, RocketMovementController rocketMovementController,
            ObjectPoolStorage objectPoolStorage)
        {
            _prefab = prefab;
            _rocketMovementController = rocketMovementController;
            _objectPoolStorage = objectPoolStorage;
        }
        public GameObject Spawn()
        {
            throw new NotImplementedException();
        }

        public void Dispose(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}