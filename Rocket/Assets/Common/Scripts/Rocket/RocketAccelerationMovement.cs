using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Input;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketAccelerationMovement : IRocketMoveComponent
    {
        private readonly RocketMovementController _rocketMovementController;
        private readonly Transform _transform;
        private readonly InputManager _inputManager;
        private readonly MeshCollider _meshCollider;
        private Vector3 _screenBounds;
        private float _xVelocity;
        private float _maxAcceleration = 0.3f;
        private float _currentAcceleration;

        private float _yRotValue = 10;
        private float _defaultYRotValue;
        private Quaternion _targetRot;


        public RocketAccelerationMovement(RocketMovementController rocketMovementController, Transform transform,
            InputManager inputManager, Vector3 screenBounds)
        {
            _rocketMovementController = rocketMovementController;
            _transform = transform;
            _inputManager = inputManager;
            _screenBounds = screenBounds;
            _meshCollider = _transform.GetComponentInChildren<MeshCollider>();
            _defaultYRotValue = transform.rotation.x;
        }
        

        private void OnInput(Vector3 accelerationValue)
        {
            Move(accelerationValue);
            
            Rotate(accelerationValue);
        }

        private void Rotate(Vector3 accelerationValue)
        {
            _targetRot.x = _transform.rotation.x;
            _targetRot.y = (_transform.rotation.y + _yRotValue) / (_screenBounds.x + _meshCollider.bounds.size.x * 2) /
                2 *  accelerationValue.x;
            _targetRot.z = _transform.rotation.z;
            _targetRot.w = _transform.rotation.w;

            _transform.rotation = _targetRot;
        }


        private float GetProcessedAcceleration(Vector3 accelerationValue)
        {
            return Mathf.Sign(-accelerationValue.x) * Mathf.Min(Mathf.Abs(-accelerationValue.x), _maxAcceleration);
        }


        private void Move(Vector3 accelerationValue)
        {
            var processedAcceleration = GetProcessedAcceleration(accelerationValue);
                
            float smoothedX = Mathf.SmoothDamp(_transform.position.x,
                (_screenBounds.x + _meshCollider.bounds.size.x * 2) / 2 * processedAcceleration / _maxAcceleration,
                ref _xVelocity, 0.2f);

            _transform.position = new Vector3(smoothedX,
                _transform.position.y, _transform.position.z);
        }


        public void Enable()
        {
            _inputManager.OnInput += OnInput;
        }

        public void Disable()
        {
            _inputManager.OnInput -= OnInput;
        }
    }
}