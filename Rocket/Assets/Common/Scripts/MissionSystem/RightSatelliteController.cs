using System;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSatelliteController: ISatelliteController
    {
        private readonly ISatelliteSpawner _rightSatelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private GameStateController _gameStateController;
        private Queue<ISatellite> _rightMovableSatellites = new Queue<ISatellite>(10);
        public ISatellite rightScopedSatellite { get; private set; }


        public RightSatelliteController(
            ISatelliteSpawner rightSatelliteSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController)
        {
            _rightSatelliteSpawner = rightSatelliteSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _rightSatelliteSpawner.Spawn();
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController,_gameStateController, DisposeLastSatellite,ScopeToNextSatellite);
            if (!SatellitesExist())
            {
                ChangeScopedSatellite(satellite);
            }
            _rightMovableSatellites.Enqueue(satellite);
        }

        public void Spawn()
        {
            CreateSatellite();
        }

        public void DisposeLastSatellite()
        {
            GameObject gameObject = _rightMovableSatellites.Dequeue().GetGameObject();
            if (_rightMovableSatellites.Count > 0)
            {
                ChangeScopedSatellite(_rightMovableSatellites.Peek());
            }
            _rightSatelliteSpawner.Dispose(gameObject);
        }

        public void Execute()
        {
            if (_rightMovableSatellites.Count > 0)
            {
                foreach (var satellite in _rightMovableSatellites.ToArray())
                {
                    satellite.Execute();
                }
            }
        }

        private void ChangeScopedSatellite(ISatellite satellite)
        {
            if (satellite != null)
            {
                rightScopedSatellite = satellite;
            }
        }

        public bool SatellitesExist()
        {
            return _rightMovableSatellites.Count > 0;
        }

        private void ScopeToNextSatellite()
        {
            var array = _rightMovableSatellites.ToArray();
            ISatellite satellite = array[array.Length - 1];
            if ( satellite != null)
            {
                rightScopedSatellite = satellite;
            }
            else
            {
                ScopeToNextSatellite();
            }
        }

    }
}