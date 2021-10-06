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
        

        public RocketAccelerationMovement(RocketMovementController rocketMovementController, Transform transform,
            InputManager inputManager, Vector3 screenBounds)
        {
            _rocketMovementController = rocketMovementController;
            _transform = transform;
            _inputManager = inputManager;
            _screenBounds = screenBounds;
            _meshCollider = _transform.GetComponentInChildren<MeshCollider>();
        }
        

        private void OnAcceleration(Vector3 accelerationValue)
        {
            float smoothedX = Mathf.SmoothDamp(_transform.position.x,
                (_screenBounds.x + _meshCollider.bounds.size.x * 2) / 2 * -accelerationValue.x, ref _xVelocity, 0.2f);
            
            _transform.position = new Vector3(smoothedX, 
                _transform.position.y, _transform.position.z);

        }

        public void Move(Action<MovementState> changeState = null)
        {
            
        }

        public void Enable()
        {
            _inputManager.OnAcceleration += OnAcceleration;
        }

        public void Disable()
        {
            _inputManager.OnAcceleration -= OnAcceleration;
        }

    }
}