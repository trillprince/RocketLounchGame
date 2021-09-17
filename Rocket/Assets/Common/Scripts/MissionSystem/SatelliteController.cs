using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteController
    {
        private readonly ISatelliteSpawner _satelliteSpawner;
        private readonly ISatelliteSpawner _satelliteSpawner2;
        private readonly RocketMovementController _rocketMovementController;
        private bool _satellitesExist = false;
        private Queue<ISatellite> _movableSatellites = new Queue<ISatellite>(10);


        public SatelliteController(
            ISatelliteSpawner satelliteSpawner,ISatelliteSpawner satelliteSpawner2,
            RocketMovementController rocketMovementController)
        {
            _satelliteSpawner = satelliteSpawner;
            _satelliteSpawner2 = satelliteSpawner2;
            _rocketMovementController = rocketMovementController;
        }

        private ISatellite CreateSatellite()
        {
            var range = Random.Range(-2, 2);
            GameObject gameObject;
            if (range < 0)
            {
                gameObject = _satelliteSpawner.Spawn();
            }
            else
            {
                gameObject = _satelliteSpawner2.Spawn();
            }
            ISatellite satellite = gameObject.GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController);
            return satellite;
        }

        public ISatellite Spawn()
        {
            ISatellite satellite = CreateSatellite();
            _movableSatellites.Enqueue(satellite);
            return satellite;
        }

        private void SatelliteStateOnPosition (ISatellite satellite)
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
                _satelliteSpawner.Dispose(satellite.GetGameObject());
                _movableSatellites.Dequeue();
            }
        }
        
        public void Execute()
        {
            if (_movableSatellites.Count > 0)
            {
                foreach (var satellite in _movableSatellites.ToArray())
                {
                    satellite.Move(SatelliteStateOnPosition);
                }
            }
        }

        public void DequeueSatellite()
        {
            _movableSatellites.Dequeue();
        }

        public bool SatellitesExist()
        {
            return _movableSatellites.Count > 0;
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