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
            satellite.Constructor(_rocketMovementController);
            leftScopedSatellite = satellite;
            _leftMovableSatellites.Enqueue(leftScopedSatellite);
        }

        public void Spawn()
        {
            CreateSatellite();
        }

        private void SatelliteStateOnPosition(ISatellite satellite)
        {
            var satellitePos = satellite.GetTransform().position;
            var screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - satellitePos.z));

            if (satellitePos.y < -screenBounds.y && satellitePos.y >= -screenBounds.y * 0.5f)
            {
                satellite.SatelliteState = SatelliteState.UpperRed;
            }
            else if (satellitePos.y < -screenBounds.y * 0.5f && satellitePos.y >= 0)
            {
                satellite.SatelliteState = SatelliteState.Yellow;
            }
            else if (satellitePos.y < 0 && satellitePos.y >= screenBounds.y * 0.5f)
            {
                satellite.SatelliteState = SatelliteState.Green;
            }
            else if (satellitePos.y < screenBounds.y * 0.5f && satellitePos.y >= screenBounds.y)
            {
                satellite.SatelliteState = SatelliteState.LoweRed;
            }
            else if (satellitePos.y < screenBounds.y - satellite.GetMeshCollider().bounds.size.y)
            {
                satellite.SatelliteState = SatelliteState.Dispose;

                _leftSatelliteSpawner.Dispose(satellite.GetGameObject());
                _leftMovableSatellites.Dequeue();
                leftScopedSatellite = _leftMovableSatellites.Peek();
            }
        }

        public void Execute()
        {
            if (_leftMovableSatellites.Count > 0)
            {
                foreach (var satellite in _leftMovableSatellites.ToArray())
                {
                    satellite.Move(SatelliteStateOnPosition);
                }
            }
        }

        public void DisposeScopedSatellite()
        {
            _leftSatelliteSpawner.Dispose(_leftMovableSatellites.Dequeue().GetGameObject());
        }

        public bool SatellitesExist()
        {
            return _leftMovableSatellites.Count > 0;
        }
    }

    public enum SatelliteState
    {
        UpperRed,
        Yellow,
        Green,
        LoweRed,
        Dispose
    }
}