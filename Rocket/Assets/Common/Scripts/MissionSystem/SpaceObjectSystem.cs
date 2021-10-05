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
    public class SpaceObjectSystem : IUpdatable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private SpawnPositionController _spawnPositionController;
        private readonly GameStateController _gameStateController;
        private readonly InputListener _inputListener;
        private bool _satelliteSystemActive;
        private ISpaceObjectController _spaceObjectController;
        private int _spawnsBeforeCheckPoint = 10;

        public SpaceObjectSystem
        (
            ICoroutineRunner coroutineRunner,
            GameStateController gameStateController,
            InputListener inputListener,
            ISpaceObjectController spaceObjectController,
            SpawnPositionController spawnPositionController
        )
        {
            _coroutineRunner = coroutineRunner;
            _gameStateController = gameStateController;
            _inputListener = inputListener;
            _spaceObjectController = spaceObjectController;
            _spawnPositionController = spawnPositionController;
            
        }

        private void StartSpawning()
        {
            Random rnd = new Random();
            ISpawnPosition[] MyRandomArray = _spawnPositionController.SpawnPositions.OrderBy(x => rnd.Next()).ToArray();

            for (int j = 0; j < _spawnPositionController.SpawnPositions.Length; j++)
            {
                _spaceObjectController.Spawn(MyRandomArray[j]);
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
            StartSpawning();
        }
    }
}