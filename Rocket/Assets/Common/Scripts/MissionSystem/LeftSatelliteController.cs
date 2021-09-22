using System;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class LeftSatelliteController: ISatelliteController
    {
        private readonly ISatelliteSpawner _leftSatelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private Queue<ISatellite> _leftMovableSatellites = new Queue<ISatellite>(10);
        public ISatellite leftScopedSatellite { get; private set; }


        public LeftSatelliteController(
            ISatelliteSpawner leftSatelliteSpawner, 
            RocketMovementController rocketMovementController,
            GameStateController gameStateController)
        {
            _leftSatelliteSpawner = leftSatelliteSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _leftSatelliteSpawner.Spawn();
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController,_gameStateController,DisposeLastSatellite,ScopeToNextSatellite);
            if (!SatellitesExist())
            {
                ChangeScopedSatellite(satellite);
            }
            _leftMovableSatellites.Enqueue(satellite);
        }

        private void ChangeScopedSatellite(ISatellite satellite)
        {
            if (satellite != null)
            {
                leftScopedSatellite = satellite;
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
            _leftSatelliteSpawner.Dispose(gameObject);
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
        private void ScopeToNextSatellite()
        {
            var array = _leftMovableSatellites.ToArray();
            ISatellite satellite = array[array.Length - 1];
            if ( satellite != null)
            {
                leftScopedSatellite = satellite;
            }
            else
            {
                ScopeToNextSatellite();
            }
        }
        
    }

}