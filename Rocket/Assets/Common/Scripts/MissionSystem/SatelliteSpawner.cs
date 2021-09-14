using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteSpawner: ISpawner
    {
        private ISatelliteFactory _satelliteFactory;
        private Vector2 _screenBounds;
        private Transform _rocketTransform;

        public SatelliteSpawner(ISatelliteFactory satelliteFactory, Transform rocketTransform,Rigidbody  rocketRigidbody)
        {
            _satelliteFactory = satelliteFactory;
            _rocketTransform = rocketTransform;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - rocketRigidbody.position.z));
        }
        
        public void Spawn()
        {
            GameObject satellite = _satelliteFactory.CreateSatellite();
            satellite.transform.position = CalculatePosition(satellite.GetComponent<MeshCollider>(),_rocketTransform);
        }
        
        private Vector3 CalculatePosition(MeshCollider meshCollider, Transform transform)
        {
            return new Vector3((_screenBounds.x - transform.position.x)/2,
                _screenBounds.y + meshCollider.bounds.size.y,transform.position.z );
        }
    }
}