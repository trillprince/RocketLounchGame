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
        private bool _spaceObjectSystemActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private ISpaceObjectLifeCycle _spaceObjectLifeCycle;
        private readonly MeshCollider _rocketMeshCollider;
        private int _spawnsBeforeCheckPoint = 25;
        private ObjectsForSpawn _objectsForSpawn;

        public SpaceObjectSpawnController
        (
            ICoroutineRunner coroutineRunner,
            ISpaceObjectLifeCycle spaceObjectLifeCycle,
            RocketMovement rocketMovement)
        {
            _objectsForSpawn = new ObjectsForSpawn(new AssetProvider());
            _coroutineRunner = coroutineRunner;
            _spaceObjectLifeCycle = spaceObjectLifeCycle;
            _rocketMeshCollider = rocketMovement.GetMeshCollider();
            _spawnPositionController = new SpawnPositionController(rocketMovement,
                new LeftSpawnPosition(rocketMovement),
                new RightSpawnPosition(rocketMovement),
                new MiddleSpawnPosition(rocketMovement));
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
                    if (randomIndexForRemove == j) continue;

                    var spaceObject = _spaceObjectLifeCycle.Spawn(shuffledArray[j], _objectsForSpawn.GetRandomObject());
                    while ((spaceObject.GetSpawnPosition().y - spaceObject.GetTransform().position.y) <
                           _rocketMeshCollider.bounds.size.y * 1.5)
                    {
                        if (!_spaceObjectSystemActive)
                        {
                            yield break;
                        }
                        yield return null;
                    }

                    lastSpawnPos = shuffledArray[j];
                }
            }
        }


        private void ShuffleIfSimilarPositions(int index, ref ISpawnPosition[] shuffledArray,
            ISpawnPosition lastSpawnPos, Random random)
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
            if (_spaceObjectSystemActive)
            {
                _spaceObjectLifeCycle.Execute();
            }
        }

        public void Disable()
        {
            _spaceObjectSystemActive = false;
            _spaceObjectLifeCycle.Disable();
        }

        public void Enable()
        {
            _spaceObjectSystemActive = true;
            _spaceObjectLifeCycle.Enable();
            _coroutineRunner.StartCoroutine(StartSpawning());
        }
    }
}