using System.Collections.Generic;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectLifeCycle : ISpaceObjectLifeCycle
    {
        private readonly IPoolWorker _spaceObjectPoolWorker;
        private readonly RocketController _rocketController;
        private readonly IGameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;

        private List<ISpaceObject> _movableSpaceObjects;
        private bool IsEnabled { get; set; }

        public SpaceObjectLifeCycle(
            IPoolWorker spaceObjectPoolWorker,
            RocketController rocketController,
            IGameStateController gameStateController,
            GameLoopController gameLoopController)
        {
            _spaceObjectPoolWorker = spaceObjectPoolWorker;
            _rocketController = rocketController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
            _movableSpaceObjects = new List<ISpaceObject>(20);
        }

        public ISpaceObject Spawn(ISpawnPosition spawnPosition, GameObject prefab)
        {
            if (IsEnabled)
            {
                ISpaceObject spaceObject = _spaceObjectPoolWorker.PopFromPool(spawnPosition, prefab)
                    .GetComponent<ISpaceObject>();
                spaceObject.Constructor(_rocketController, this, _gameLoopController, _gameStateController,
                    spawnPosition);
                _movableSpaceObjects.Add(spaceObject);
                return spaceObject;
            }

            return default;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
            for (int i = 0; i < _movableSpaceObjects.Count; i++)
            {
                _spaceObjectPoolWorker.Dispose(_movableSpaceObjects[i]);
            }
            _movableSpaceObjects.Clear();
        }


        public void Execute()
        {
            for (int i = 0; i < _movableSpaceObjects.Count; i++)
            {
                _movableSpaceObjects[i].Execute();
            }
        }

        public void Dispose(ISpaceObject spaceObject)
        {
            _movableSpaceObjects.Remove(spaceObject);
            _spaceObjectPoolWorker.Dispose(spaceObject);
        }
    }
}