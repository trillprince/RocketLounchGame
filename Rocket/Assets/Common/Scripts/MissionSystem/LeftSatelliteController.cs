using System;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class LeftSatelliteController
    {
        private readonly ISatelliteSpawner _leftSatelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private bool _satellitesExist = false;
        private Queue<ISatellite> _leftMovableSatellites = new Queue<ISatellite>(10);
        public ISatellite leftScopedSatellite { get; private set; }


        public LeftSatelliteController(
            ISatelliteSpawner leftSatelliteSpawner, 
            RocketMovementController rocketMovementController)
        {
            _leftSatelliteSpawner = leftSatelliteSpawner;
            _rocketMovementController = rocketMovementController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _leftSatelliteSpawner.Spawn();
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController,DisposeLastSatellite);
            if (_leftMovableSatellites.Count < 2)
            {
                leftScopedSatellite = satellite;
            }
            _leftMovableSatellites.Enqueue(leftScopedSatellite);
        }

        public void Spawn()
        {
            CreateSatellite();
        }


        public void DisposeLastSatellite()
        {
            GameObject gameObject = _leftMovableSatellites.Dequeue().GetGameObject();
            if (_leftMovableSatellites.Count > 1)
            {
                leftScopedSatellite = _leftMovableSatellites.Peek();
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
    }

}