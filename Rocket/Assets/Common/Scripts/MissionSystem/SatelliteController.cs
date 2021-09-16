using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteController : ISatelliteController
    {
        private readonly ISatelliteSpawner _satelliteSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private Queue<ISatellite> _satellitesToMove = new Queue<ISatellite>();
        private bool _satellitesExist = false;
        private ISatellite _lastSatellite;
        private GameObject _lastSatelliteObject;

        public SatelliteController(
            ISatelliteSpawner satelliteSpawner,
            RocketMovementController rocketMovementController)
        {
            _satelliteSpawner = satelliteSpawner;
            _rocketMovementController = rocketMovementController;
        }

        private ISatellite CreateSatellite()
        {
            _lastSatelliteObject = _satelliteSpawner.Spawn();
            ISatellite satellite = _lastSatelliteObject.GetComponent<ISatellite>();
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
            var satellitePos = satellite.GetTransform().position;
            var screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - satellitePos.z));
            
            if (satellitePos.y <  - screenBounds.y && satellitePos.y >= - screenBounds.y * 0.5f)
            {
                Debug.Log("111111111111111");
            }
            else if (satellitePos.y < - screenBounds.y * 0.5f && satellitePos.y >= 0)
            {
                Debug.Log("222222222222222");
            }
            else if (satellitePos.y < 0 && satellitePos.y >=  screenBounds.y * 0.5f)
            {
                Debug.Log("333333333333333");
            }
            else if (satellitePos.y <  screenBounds.y * 0.5f && satellitePos.y >=  screenBounds.y)
            {
                Debug.Log("44444444444444444");
            }
            else if (satellitePos.y < screenBounds.y)
            {
                
            }
            
        }

        public void Execute()
        {
            if (_satellitesExist)
            {
                foreach (var satellite in _satellitesToMove)
                {
                    satellite.Move();
                    SatelliteStateOnPosition(satellite);
                }
            }
        }
        void OnDrawGizmos()
        {
            var screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    0));
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(screenBounds, 2);
        }
    }
}