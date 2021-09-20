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
            satellite.Constructor(_rocketMovementController);
            rightScopedSatellite = satellite;
            _rightMovableSatellites.Enqueue(rightScopedSatellite);
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

                _rightSatelliteSpawner.Dispose(satellite.GetGameObject());
                _rightMovableSatellites.Dequeue();
                rightScopedSatellite = _rightMovableSatellites.Peek();
            }
        }

        public void Execute()
        {
            if (_rightMovableSatellites.Count > 0)
            {
                foreach (var satellite in _rightMovableSatellites.ToArray())
                {
                    satellite.Move(SatelliteStateOnPosition);
                }
            }
        }

        public void DisposeScopedSatellite()
        {
            _rightSatelliteSpawner.Dispose(_rightMovableSatellites.Dequeue().GetGameObject());
        }

        public bool SatellitesExist()
        {
            return _rightMovableSatellites.Count > 0;
        }
    }
}