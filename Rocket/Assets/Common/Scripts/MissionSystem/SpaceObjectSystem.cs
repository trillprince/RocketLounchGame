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
        private readonly SatelliteStateChanger _satelliteStateChanger;
        private bool _satelliteSystemActive;

        private ISpaceObjectController[] _spaceObjectControllers;
        private ISpawnPosition[] _spawnPositions;
        private float _waitTimeBeforeStart = 4;
        private float _waitTimeBeforeSpawn = 1;

        public SpaceObjectSystem
        (
            RocketMovementController rocketMovementController,
            ICoroutineRunner coroutineRunner,
            GameStateController gameStateController,
            InputListener inputListener,
            SatelliteStateChanger satelliteStateChanger,
            params ISpaceObjectController [] spaceObjectControllers
        )
        {
            _coroutineRunner = coroutineRunner;
            _gameStateController = gameStateController;
            _inputListener = inputListener;
            _satelliteStateChanger = satelliteStateChanger;
            _spaceObjectControllers = spaceObjectControllers;
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
            var randomIndex = Random.Range(0, _spaceObjectControllers.Length);
            for (int i = 0; i < _spaceObjectControllers.Length; i++)
            {
                if (i == randomIndex)
                {
                    continue;
                }
                _spaceObjectControllers[i].Spawn(_spawnPositions[i]);
            }
        }

        public void Execute()
        {
            if (_satelliteSystemActive)
            {
                foreach (var spaceObjectController in _spaceObjectControllers)
                {
                    spaceObjectController.Execute();
                }
                _satelliteStateChanger.Execute();
            }
        }

        public void Disable()
        {
            _satelliteSystemActive = false;
            foreach (var spaceObjectController in _spaceObjectControllers)
            {
                spaceObjectController.Disable();
            }
            _satelliteStateChanger.Disable();
        }

        public void Enable()
        {
            _satelliteSystemActive = true;
            foreach (var spaceObjectController in _spaceObjectControllers)
            {
                spaceObjectController.Enable();
            }
            _satelliteStateChanger.Enable();
            _coroutineRunner.StartCoroutine(WaitBeforeSpawn(_waitTimeBeforeSpawn));
        }
    }
}