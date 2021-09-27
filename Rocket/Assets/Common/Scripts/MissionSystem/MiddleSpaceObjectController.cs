using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpaceObjectController: IAsteroidController
    {
        private readonly ISpaceObjectSpawner _middleSpaceObjectSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;
        private Queue<ISpaceObject> _middleMovableSpaceObjects = new Queue<ISpaceObject>(10);
        public ISpaceObject RightScopedSpaceObject { get; private set; }
        private bool IsEnabled { get; set; }
        
        public MiddleSpaceObjectController(
            ISpaceObjectSpawner middleSpaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController)
        {
            _middleSpaceObjectSpawner = middleSpaceObjectSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
        }
        private void CreateSpaceObject()
        {
            GameObject gameObject;
            gameObject = _middleSpaceObjectSpawner.Spawn();
            IAsteroid asteroid = gameObject.GetComponent<IAsteroid>();
            asteroid.Constructor(_rocketMovementController,_gameStateController,this,_gameLoopController);
            if (!AsteroidsExist())
            {
                ChangeScopedAsteroid(asteroid);
            }
            _middleMovableSpaceObjects.Enqueue(asteroid);
        }

        public void Spawn()
        {
            if (IsEnabled)
            {
                CreateSpaceObject();
            }
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void DisposeLastObject()
        {
            GameObject gameObject = _middleMovableSpaceObjects.Dequeue().GetGameObject();
            if (_middleMovableSpaceObjects.Count > 0)
            {
                ChangeScopedAsteroid(_middleMovableSpaceObjects.Peek());
            }
            _middleSpaceObjectSpawner.Dispose(gameObject);
        }

        public void Disable()
        {
            IsEnabled = false;
            for (int i = 0; i < _middleMovableSpaceObjects.Count; i++)
            {
                _middleSpaceObjectSpawner.Dispose(_middleMovableSpaceObjects.Dequeue().GetGameObject());
            }
        }


        public void Execute()
        {
            if (_middleMovableSpaceObjects.Count > 0)
            {
                foreach (var satellite in _middleMovableSpaceObjects.ToArray())
                {
                    satellite.Execute();
                }
            }
        }

        private void ChangeScopedAsteroid(ISpaceObject spaceObject)
        {
            if (spaceObject != null)
            {
                RightScopedSpaceObject = spaceObject;
            }
        }

        public bool AsteroidsExist()
        {
            return _middleMovableSpaceObjects.Count > 0;
        }

        public void ScopeToNextObject()
        {
            var array = _middleMovableSpaceObjects.ToArray();
            ISpaceObject spaceObject = array[array.Length - 1];
            if ( spaceObject != null)
            {
                RightScopedSpaceObject = spaceObject;
            }
            else
            {
                ScopeToNextObject();
            }
        }

    }
}