using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteController : ISatelliteController
    {
        private readonly ISatelliteSpawner _satelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private Queue <ISatellite> _satellitesToMove = new Queue<ISatellite>();
        private bool _satellitesExist = false;
        private ISatellite _lastSatellite;

        public SatelliteController(
            ISatelliteSpawner satelliteSpawner,
            RocketMovementController rocketMovementController)
        {
            _satelliteSpawner = satelliteSpawner;
            _rocketMovementController = rocketMovementController;
        }

        private ISatellite CreateSatellite()
        {
            ISatellite satellite = _satelliteSpawner.Spawn().GetComponent<ISatellite>();
            satellite.Constructor(_rocketMovementController);
            return satellite;
        }

        public void Enable()
        {
            _lastSatellite = CreateSatellite();
            _satellitesToMove.Enqueue(_lastSatellite);
            _satellitesExist = true;
        }

        private void SatelliteStateOnPosition(ISatellite satellite)
        {
            var satelliteTransform = satellite.Move();
            if ((satelliteTransform.position.y - _satelliteSpawner.LastSpawnPos.y + satellite.GetMeshCollider().bounds.size.y) < 10)
            {
                Debug.Log("aye");
            }
        }
        
        public void Execute()
        {
            if (_satellitesExist)
            {
                foreach (var satellite in _satellitesToMove)
                {
                    SatelliteStateOnPosition(satellite);
                }
            }
        }
    }
}