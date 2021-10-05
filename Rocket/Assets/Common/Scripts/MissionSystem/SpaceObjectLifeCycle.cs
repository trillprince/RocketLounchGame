using System.Collections.Generic;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectLifeCycle: ISpaceObjectController
    {
        private readonly IPoolWorker _spaceObjectPoolWorker;
        private readonly RocketMovementController _rocketMovementController;
        private readonly GameLoopController _gameLoopController;
        
        private Queue<ISpaceObject> _movableSpaceObjects;
        private bool IsEnabled { get; set; }

        public SpaceObjectLifeCycle(
            IPoolWorker spaceObjectPoolWorker,
            RocketMovementController rocketMovementController,
            GameLoopController gameLoopController)
        {
            _spaceObjectPoolWorker = spaceObjectPoolWorker;
            _rocketMovementController = rocketMovementController;
            _gameLoopController = gameLoopController;
            _movableSpaceObjects = new Queue<ISpaceObject>(20);
        }

        public ISpaceObject Spawn(ISpawnPosition spawnPosition)
        {
            if (IsEnabled)
            {
                ISpaceObject spaceObject = _spaceObjectPoolWorker.PopFromPool(spawnPosition).GetComponent<ISpaceObject>();
                spaceObject.Constructor(_rocketMovementController,this,_gameLoopController,spawnPosition);
                _movableSpaceObjects.Enqueue(spaceObject);
                return spaceObject;
            }
            return default;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void DisposeLastObject()
        {
            GameObject gameObject = _movableSpaceObjects.Dequeue().GetGameObject();
            _spaceObjectPoolWorker.Dispose(gameObject);
        }

        public void Disable()
        {
            IsEnabled = false;
            for (int i = 0; i < _movableSpaceObjects.Count; i++)
            {
                _spaceObjectPoolWorker.Dispose(_movableSpaceObjects.Dequeue().GetGameObject());
            }
        }


        public void Execute()
        {
            if (_movableSpaceObjects.Count > 0)
            {
                foreach (var spaceObject in _movableSpaceObjects.ToArray())
                {
                    spaceObject.Execute();
                }
            }
        }
        

    }
}