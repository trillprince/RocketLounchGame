using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSpawnController : IUpdatable
    {
        private readonly SpawnPositionController _spawnPositionController;
        private bool _spaceObjectSystemActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
        private readonly ILevelInfo _levelInfo;
        private readonly MeshCollider _rocketMeshCollider;
        private int _spawnsBeforeCheckPoint = 15;
        private readonly ObjectsForSpawn _objectsForSpawn;
        private ISpawnPosition _removedSpawnPos;
        private ISpawnPosition _lastSpawnPos;
        private ISpaceObject _lastSpawnedSpaceObject;
        private int _maxCoinsPerSpawn = 3;
        private int _spawnPosForCoin;
        private ISpaceObject _coin;

        public SpaceObjectSpawnController
        (
            ICoroutineRunner coroutineRunner,
            ISpaceObjectLifeCycle spaceObjectLifeCycle,
            RocketMovement rocketMovement,
            ILevelInfo levelInfo)
        {
            _objectsForSpawn = new ObjectsForSpawn(new AssetProvider());
            _coroutineRunner = coroutineRunner;
            _spaceObjectLifeCycle = spaceObjectLifeCycle;
            _levelInfo = levelInfo;
            _rocketMeshCollider = rocketMovement.GetMeshCollider();
            _spawnPositionController = new SpawnPositionController(rocketMovement,
                new LeftSpawnPosition(rocketMovement),
                new RightSpawnPosition(rocketMovement),
                new MiddleSpawnPosition(rocketMovement));
        }

        private IEnumerator SpawnLoop()
        {
            _levelInfo.NextLevel();
            Random random = new Random();
            ISpawnPosition[] shuffledArray =
                _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();

            FillSpawnLapInfo(shuffledArray);
            SetSpawnsCount();
            for (int i = 0; i < _spawnsBeforeCheckPoint; i++)
            {
                ShuffleOnSimilarPositions(i, ref shuffledArray, random);
                FillSpawnLapInfo(shuffledArray);
                for (int j = 0; j < shuffledArray.Length; j++)
                {
                    if (shuffledArray[j] == _removedSpawnPos)
                    {
                        while (ObjectCloseToSpawnPoint(_lastSpawnedSpaceObject, 2))
                        {
                            yield return null;
                        }

                        continue;
                    }

                    var spaceObject = SpawnSpaceObject(shuffledArray, j);
                    SpawnCoin();
                    _lastSpawnedSpaceObject = spaceObject;
                    while (ObjectCloseToSpawnPoint(spaceObject, 3))
                    {
                        if (!_spaceObjectSystemActive)
                        {
                            yield break;
                        }

                        yield return null;
                    }

                    _lastSpawnPos = shuffledArray[j];
                }
            }

            yield return new WaitForSeconds(5);
            _coroutineRunner.StartCoroutine(SpawnLoop());
        }

        private ISpaceObject SpawnSpaceObject(ISpawnPosition[] shuffledArray, int j)
        {
            var spaceObject = _spaceObjectLifeCycle.Spawn(shuffledArray[j], _objectsForSpawn.GetRandomObject());
            return spaceObject;
        }

        private void SpawnCoin()
        {
            var spawnedCoin = _spaceObjectLifeCycle.Spawn(_removedSpawnPos, _objectsForSpawn.GetCoin());
            if (_coin != null)
            {
                spawnedCoin.GetTransform().rotation = _coin.GetTransform().rotation;
            }
            _coin = spawnedCoin;
        }

        private void FillSpawnLapInfo(ISpawnPosition[] shuffledArray)
        {
            _removedSpawnPos = shuffledArray[UnityRandom.Range(1, shuffledArray.Length - 1)];
            _lastSpawnPos = shuffledArray[shuffledArray.Length - 1];
        }

        private bool ObjectCloseToSpawnPoint(ISpaceObject spaceObject, int distanceMultiplayer)
        {
            return (spaceObject.GetSpawnPosition().y - spaceObject.GetTransform().position.y) <
                   _rocketMeshCollider.bounds.size.y * distanceMultiplayer;
        }

        private void SetSpawnsCount()
        {
            if (_levelInfo.GetLevelNumber() > 0)
            {
                _spawnsBeforeCheckPoint += 2 * _levelInfo.GetLevelNumber();
            }
            else
            {
                _spawnsBeforeCheckPoint = 4;
            }
        }

        private void ShuffleOnSimilarPositions(int index, ref ISpawnPosition[] shuffledArray, Random random)
        {
            if (index > 0)
            {
                shuffledArray = _spawnPositionController.SpawnPositions.OrderBy(x => random.Next()).ToArray();
                while (shuffledArray[0] == _lastSpawnPos || shuffledArray[0] == _removedSpawnPos)
                {
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
            _coroutineRunner.StartCoroutine(SpawnLoop());
        }
    }
}