using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSpawnController : IUpdatable
    {
        private SpawnPositionController _spawnPositionController;
        private bool _satelliteSystemActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private ISpaceObjectController _spaceObjectController;
        private readonly MeshCollider _rocketMeshCollider;
        private int _spawnsBeforeCheckPoint = 25;

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
                new LeftSpawnPosition(rocketMovementController, asteroidCollider),
                new RightSpawnPosition(rocketMovementController, asteroidCollider),
                new MiddleSpawnPosition(rocketMovementController, asteroidCollider), asteroidCollider);
        }

        private IEnumerator StartSpawning()
        {
            Random random = new Random();
            ISpawnPosition[] shuffledArray =
                _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();
            var lastSpawnPos = shuffledArray[shuffledArray.Length - 1];
            for (int i = 0; i < _spawnsBeforeCheckPoint; i++)
            {
                var randomIndexForRemove = UnityRandom.Range(0, shuffledArray.Length);
                ShuffleIfSimilarPositions(i, ref shuffledArray, lastSpawnPos, random);
                for (int j = 0; j < shuffledArray.Length; j++)
                {
                    if(randomIndexForRemove == j) continue; 
                    var spaceObject = _spaceObjectController.Spawn(shuffledArray[j]);
                    while ((spaceObject.GetSpawnPosition().y - spaceObject.GetTransform().position.y) <
                           _rocketMeshCollider.bounds.size.y * 1.5)
                    {
                        yield return null;
                    }
                    lastSpawnPos = shuffledArray[j];
                }
                
            }
        }
        

        private void ShuffleIfSimilarPositions(int index, ref ISpawnPosition[] shuffledArray,ISpawnPosition lastSpawnPos,Random random)
        {
            if (index > 0)
            {
                shuffledArray = _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();
                while (shuffledArray[0] == lastSpawnPos)
                {
                    Debug.Log("resort");
                    shuffledArray = _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();
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