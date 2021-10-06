using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = System.Random;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSpawnController : IUpdatable
    {
        private SpawnPositionController _spawnPositionController;
        private bool _satelliteSystemActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private ISpaceObjectController _spaceObjectController;
        private readonly MeshCollider _rocketMeshCollider;
        private int _spawnsBeforeCheckPoint = 20;

        public SpaceObjectSpawnController
        (
            ICoroutineRunner coroutineRunner,
            ISpaceObjectController spaceObjectController,
            RocketMovementController rocketMovementController,
            SphereCollider asteroidCollider)
        {
            _coroutineRunner = coroutineRunner;
            _spaceObjectController = spaceObjectController;
            _rocketMeshCollider = rocketMovementController.GetComponentInChildren<MeshCollider>();
            _spawnPositionController = new SpawnPositionController(rocketMovementController,
                new LeftSpawnPosition(rocketMovementController,asteroidCollider),
                new RightSpawnPosition(rocketMovementController,asteroidCollider),
                new MiddleSpawnPosition(rocketMovementController,asteroidCollider), asteroidCollider);
        }

        private IEnumerator StartSpawning()
        {
            Random random = new Random();
            for (int i = 0; i < _spawnsBeforeCheckPoint; i++)
            {
                ISpawnPosition[] myRandomArray = _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();
                for (int j = 0; j < myRandomArray.Length; j++)
                {
                    var spaceObject = _spaceObjectController.Spawn(myRandomArray[j]);
                    while ((spaceObject.GetSpawnPosition().y - spaceObject.GetTransform().position.y) < _rocketMeshCollider.bounds.size.y)  
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