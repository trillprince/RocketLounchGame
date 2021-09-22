using System;
using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : IUpdatable
    {
        private float _rotateSpeed = 50f;
        private int _zRot;
        private ISatellite _satellite;
        private readonly ObjectPool _objectPool;
        private readonly GameObject _thisGameObject;
        private Transform _transform;
        private float _lerpDuration = 0.5f;
        private float _timeElapsed;

        public CargoMovement(Transform transform, ISatellite satellite,ObjectPool objectPool,GameObject thisGameObject)
        {
            _transform = transform;
            _satellite = satellite;
            _objectPool = objectPool;
            _thisGameObject = thisGameObject;
            SetRandomRot();
        }
   
        void CargoMove()
        {
            if (_timeElapsed < _lerpDuration)
            {
                var x = Mathf.Lerp(_transform.position.x, _satellite.GetTransform().position.x, _timeElapsed/_lerpDuration);
                var y = Mathf.Lerp(_transform.position.y, _satellite.GetTransform().position.y, _timeElapsed/_lerpDuration);
                var z= Mathf.Lerp(_transform.position.z, _satellite.GetTransform().position.z, _timeElapsed/_lerpDuration);
                _transform.position = new Vector3(x, y, z);
            }
            else
            {
                _objectPool.Push(_thisGameObject);
            }
            _timeElapsed += Time.deltaTime;
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

        public void Execute()
        {
            CargoMove();
            CargoRotate();
        }
    }
}
