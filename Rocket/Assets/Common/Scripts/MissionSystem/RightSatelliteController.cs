using System;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSatelliteController
    {
        private readonly ISatelliteSpawner _rightSatelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private bool _satellitesExist = false;
        private Queue<ISatellite> _rightMovableSatellites = new Queue<ISatellite>(10);
        public ISatellite rightScopedSatellite { get; private set; }


        public RightSatelliteController(
            ISatelliteSpawner rightSatelliteSpawner,
            RocketMovementController rocketMovementController)
        {
            _rightSatelliteSpawner = rightSatelliteSpawner;
            _rocketMovementController = rocketMovementController;
        }

        private void CreateSatellite()
        {
            GameObject gameObject;
            gameObject = _rightSatelliteSpawner.Spawn();
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController, DisposeLastSatellite);
            if (_rightMovableSatellites.Count < 2)
            {
                rightScopedSatellite = satellite;
            }

            _rightMovableSatellites.Enqueue(rightScopedSatellite);
        }

        public void Spawn()
        {
            CreateSatellite();
        }

        private void DisposeLastSatellite()
        {
            GameObject gameObject = _rightMovableSatellites.Dequeue().GetGameObject();
            if (_rightMovableSatellites.Count > 1)
            {
                rightScopedSatellite = _rightMovableSatellites.Peek();
            }
            _rightSatelliteSpawner.Dispose(gameObject);
        }

        public void Execute()
        {
            if (_rightMovableSatellites.Count > 0)
            {
                foreach (var satellite in _rightMovableSatellites.ToArray())
                {
                    satellite.Move();
                    satellite.StateCheck();
                }
            }
        }

        public bool SatellitesExist()
        {
            return _rightMovableSatellites.Count > 0;
        }
    }
}