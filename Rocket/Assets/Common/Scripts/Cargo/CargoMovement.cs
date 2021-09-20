using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : IUpdatable
    {
        private int _smoothness = 7;
        private float _rotateSpeed = 50f;
        private int _zRot;
        private ISatellite _satellite;
        private Transform _transform;

        public CargoMovement(Transform transform, ISatellite satellite)
        {
            _transform = transform;
            _satellite = satellite;
            SetRandomRot();
        }
   
        void CargoMove()
        {
            _transform.Translate(_satellite.GetTransform().position * Time.deltaTime, Space.World);
        }

        void CargoRotate()
        {
            _transform.Rotate(
                _transform.rotation.x,
                _transform.rotation.y, 
                _zRot * _rotateSpeed * Time.deltaTime
                );
        }

        private void SetRandomRot()
        {
            _zRot = Random.value < 0.5f ? -1 : 1;
        }

        public void Update()
        {
            CargoMove();
            CargoRotate();
        }
    }
}
