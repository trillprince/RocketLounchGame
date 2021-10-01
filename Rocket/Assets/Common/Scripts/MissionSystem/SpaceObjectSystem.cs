using System.Collections;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSystem : ISpaceObjectSystem
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStateController _gameStateController;
        private readonly InputListener _inputListener;
        private bool _satelliteSystemActive;

        private ISpaceObjectController _spaceObjectController;
        private ISpawnPosition[] _spawnPositions;
        private float _waitTimeBeforeStart = 4;
        private float _waitTimeBeforeSpawn = 1;

        public SpaceObjectSystem
        (
            RocketMovementController rocketMovementController,
            ICoroutineRunner coroutineRunner,
            GameStateController gameStateController,
            InputListener inputListener,
            ISpaceObjectController  spaceObjectController
        )
        {
            _coroutineRunner = coroutineRunner;
            _gameStateController = gameStateController;
            _inputListener = inputListener;
            _spaceObjectController = spaceObjectController;
            _spawnPositions = new ISpawnPosition[]
            {
                new LeftSpawnPosition(rocketMovementController),
                new RightSpawnPosition(rocketMovementController),
                new MiddleSpawnPosition(rocketMovementController)
            };
        }
        
        private IEnumerator WaitBeforeSpawn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (_gameStateController.CurrentGameState != GameState.CargoDrop)
            {
                yield break;
            }
            SpawnSpaceObjects();
            _coroutineRunner.StartCoroutine(WaitBeforeSpawn(_waitTimeBeforeSpawn));
        }

        public void SpawnSpaceObjects()
        {
            var randomIndex = Random.Range(0, _spawnPositions.Length);
            for (int i = 0; i < _spawnPositions.Length; i++)
            {
                if (i == randomIndex)
                {
                    continue;
                }
                _spaceObjectController.Spawn(_spawnPositions[i]);
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
            _coroutineRunner.StartCoroutine(WaitBeforeSpawn(_waitTimeBeforeSpawn));
        }
    }
}