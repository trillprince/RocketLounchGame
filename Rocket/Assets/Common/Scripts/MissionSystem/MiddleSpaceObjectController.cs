using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpaceObjectController: ISpaceObjectController
    {
        private readonly ISpaceObjectSpawner _middleSpaceObjectSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;
        private Queue<ISpaceObject> _middleMovableSpaceObjects = new Queue<ISpaceObject>(10);
        public ISpaceObject RightScopedSpaceObject { get; private set; }

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
            ISpaceObject spaceObject = gameObject.GetComponent<ISpaceObject>();
            spaceObject.Constructor(_rocketMovementController,_gameStateController,this,_gameLoopController);
            if (!SatellitesExist())
            {
                ChangeScopedSatellite(spaceObject);
            }
            _middleMovableSpaceObjects.Enqueue(spaceObject);
        }

        public void Spawn()
        {
            CreateSpaceObject();
        }

        public void DisposeLastSatellite()
        {
            GameObject gameObject = _middleMovableSpaceObjects.Dequeue().GetGameObject();
            if (_middleMovableSpaceObjects.Count > 0)
            {
                ChangeScopedSatellite(_middleMovableSpaceObjects.Peek());
            }
            _middleSpaceObjectSpawner.Dispose(gameObject);
        }

        public void DisposeAll()
        {
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

        private void ChangeScopedSatellite(ISpaceObject spaceObject)
        {
            if (spaceObject != null)
            {
                RightScopedSpaceObject = spaceObject;
            }
        }

        public bool SatellitesExist()
        {
            return _middleMovableSpaceObjects.Count > 0;
        }

        public void ScopeToNextSatellite()
        {
            var array = _middleMovableSpaceObjects.ToArray();
            ISpaceObject spaceObject = array[array.Length - 1];
            if ( spaceObject != null)
            {
                RightScopedSpaceObject = spaceObject;
            }
            else
            {
                ScopeToNextSatellite();
            }
        }

    }
}