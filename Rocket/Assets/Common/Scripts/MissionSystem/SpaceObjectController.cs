using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectController: ISpaceObjectController
    {
        private readonly ISpaceObjectFactory _spaceObjectFactory;
        private readonly RocketMovementController _rocketMovementController;
        private readonly GameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;
        
        private Queue<ISpaceObject> _movableSpaceObjects;
        private ISpaceObject ScopedSpaceObject { get; set; }
        private bool IsEnabled { get; set; }

        public SpaceObjectController(
            ISpaceObjectFactory spaceObjectFactory,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController,
            Queue<ISpaceObject> spaceObjects)
        {
            _spaceObjectFactory = spaceObjectFactory;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
            _movableSpaceObjects = spaceObjects;
        }

       private void CreateSpaceObject(ISpawnPosition spawnPosition)
        {
            GameObject gameObject;
            gameObject = _spaceObjectFactory.Spawn(spawnPosition);
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController,_gameStateController,this,_gameLoopController);
            if (!SpaceObjectExist())
            {
                ChangeScopedObject(satellite);
            }
            _movableSpaceObjects.Enqueue(satellite);
        }

        public void Spawn(ISpawnPosition spawnPosition)
        {
            if (IsEnabled)
            {
                CreateSpaceObject(spawnPosition);
            }
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void DisposeLastObject()
        {
            GameObject gameObject = _movableSpaceObjects.Dequeue().GetGameObject();
            if (_movableSpaceObjects.Count > 0)
            {
                ChangeScopedObject(_movableSpaceObjects.Peek());
            }
            _spaceObjectFactory.Dispose(gameObject);
        }

        public bool ObjectExist()
        {
            return _movableSpaceObjects.Count > 0;
        }

        public void Disable()
        {
            IsEnabled = false;
            for (int i = 0; i < _movableSpaceObjects.Count; i++)
            {
                _spaceObjectFactory.Dispose(_movableSpaceObjects.Dequeue().GetGameObject());
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

        private void ChangeScopedObject(ISpaceObject spaceObject)
        {
            if (spaceObject != null)
            {
                ScopedSpaceObject = spaceObject;
            }
        }

        private bool SpaceObjectExist()
        {
            return _movableSpaceObjects.Count > 0;
        }

        public void ScopeToNextObject()
        {
            var array = _movableSpaceObjects.ToArray();
            ISpaceObject spaceObject = array[array.Length - 1];
            if ( spaceObject != null)
            {
                ScopedSpaceObject = spaceObject;
            }
            else
            {
                ScopeToNextObject();
            }
        }
        

    }
}