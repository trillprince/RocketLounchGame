using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSpawnController : IUpdatable
    {
        private SpawnPositionController _spawnPositionController;
        private bool _satelliteSystemActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private ISpaceObjectController _spaceObjectController;
        private readonly MeshCollider _rocketMeshCollider;
        private int _spawnsBeforeCheckPoint = 10;

        public SpaceObjectSpawnController
        (
            ICoroutineRunner coroutineRunner,
            ISpaceObjectController spaceObjectController,
            RocketMovementController rocketMovementController,
            MeshCollider asteroidMeshCollider)
        {
            _coroutineRunner = coroutineRunner;
            _spaceObjectController = spaceObjectController;
            _rocketMeshCollider = rocketMovementController.GetComponentInChildren<MeshCollider>();
            _spawnPositionController = new SpawnPositionController(rocketMovementController,
                new LeftSpawnPosition(rocketMovementController, asteroidMeshCollider),
                new RightSpawnPosition(rocketMovementController, asteroidMeshCollider),
                new MiddleSpawnPosition(rocketMovementController, asteroidMeshCollider), asteroidMeshCollider);
        }

        private IEnumerator StartSpawning()
        {
            for (int i = 0; i < _spawnsBeforeCheckPoint; i++)
            {
                for (int j = 0; j < _spawnPositionController.SpawnPositions.Length; j++)
                {
                    var spaceObject = _spaceObjectController.Spawn(_spawnPositionController.SpawnPositions[Random.Range(0,_spawnPositionController.SpawnPositions.Length)]);
                    while ((spaceObject.GetSpawnPosition().y - spaceObject.GetTransform().position.y) < _rocketMeshCollider.bounds.size.y * 1.5)
                    {
                        yield return null;
                    }
                }
            }
        }


        public void Execute()
        {
            if (_satelliteSystemActive)
            {
                _spaceObjectController.Execute();
            }
        }

        public void Disable()
        {
            _satelliteSystemActive = false;
            _spaceObjectController.Disable();
        }

        public void Enable()
        {
            _satelliteSystemActive = true;
            _spaceObjectController.Enable();
            _coroutineRunner.StartCoroutine(StartSpawning());
        }
    }
}