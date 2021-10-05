using System;
using Common.Scripts.Planet;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : IUpdatable
    {
        private float _rotateSpeed = 50f;
        private int _zRot;
        private ISpaceObject _spaceObject;
        private readonly ObjectPool _objectPool;
        private readonly GameObject _thisGameObject;
        private Transform _transform;
        private float _lerpDuration = 0.5f;
        private float _timeElapsed;

        public CargoMovement(Transform transform, ISpaceObject spaceObject,ObjectPool objectPool,GameObject thisGameObject)
        {
            _transform = transform;
            _spaceObject = spaceObject;
            _objectPool = objectPool;
            _thisGameObject = thisGameObject;
            SetRandomRot();
        }
   
        void CargoMove()
        {
            if (_timeElapsed < _lerpDuration)
            {
                var x = Mathf.Lerp(_transform.position.x, _spaceObject.GetTransform().position.x, _timeElapsed/_lerpDuration);
                var y = Mathf.Lerp(_transform.position.y, _spaceObject.GetTransform().position.y, _timeElapsed/_lerpDuration);
                var z= Mathf.Lerp(_transform.position.z, _spaceObject.GetTransform().position.z, _timeElapsed/_lerpDuration);
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
