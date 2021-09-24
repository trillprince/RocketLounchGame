using System;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpaceObjectController: ISatelliteController
    {
        private readonly ISpaceObjectSpawner _leftSpaceObjectSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private Queue<ISatellite> _leftMovableSatellites = new Queue<ISatellite>(10);
        private GameLoopController _gameLoopController;
        public ISatellite LeftScopedSpaceObject { get; private set; }


        public LeftSpaceObjectController(
            ISpaceObjectSpawner leftSpaceObjectSpawner, 
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController)
        {
            _leftSpaceObjectSpawner = leftSpaceObjectSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _leftSpaceObjectSpawner.Spawn();
            ISatellite spaceObject = gameObject.GetComponent<ISatellite>();
            spaceObject.Constructor(_rocketMovementController,_gameStateController,this,_gameLoopController);
            if (!SatellitesExist())
            {
                ChangeScopedSatellite(spaceObject);
            }
            _leftMovableSatellites.Enqueue(spaceObject);
        }

        private void ChangeScopedSatellite(ISatellite spaceObject)
        {
            if (spaceObject != null)
            {
                LeftScopedSpaceObject = spaceObject;
            }
        }

        public void Spawn()
        {
            CreateSatellite();
        }
        
        public void DisposeLastSatellite()
        {
            GameObject gameObject = _leftMovableSatellites.Dequeue().GetGameObject();
            if (_leftMovableSatellites.Count > 0)
            {
                ChangeScopedSatellite(_leftMovableSatellites.Peek());
            }
            _leftSpaceObjectSpawner.Dispose(gameObject);
        }

        public void DisposeAll()
        {
            for (int i = 0; i < _leftMovableSatellites.Count; i++)
            {
                _leftSpaceObjectSpawner.Dispose(_leftMovableSatellites.Dequeue().GetGameObject());
            }
        }

        public void Execute()
        {
            if (_leftMovableSatellites.Count > 0)
            {
                foreach (var satellite in _leftMovableSatellites.ToArray())
                {
                    satellite.Execute();
                }
            }
        }

        public bool SatellitesExist()
        {
            return _leftMovableSatellites.Count > 0;
        }
        public void ScopeToNextSatellite()
        {
            var array = _leftMovableSatellites.ToArray();
            ISatellite spaceObject = array[array.Length - 1];
            if ( spaceObject != null)
            {
                LeftScopedSpaceObject = spaceObject;
            }
            else
            {
                ScopeToNextSatellite();
            }
        }
        
    }

}