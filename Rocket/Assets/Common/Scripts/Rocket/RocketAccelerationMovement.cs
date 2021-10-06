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
            _transform.position = new Vector3((_screenBounds.x + _meshCollider.bounds.size.x * 2) /2 * - (float) Math.Round(accelerationValue.x * 1000f) / 1000f, 
                _transform.position.y, _transform.position.z);
            Debug.Log($"Acceleration {accelerationValue} , {(float) Math.Round(accelerationValue.x * 1000f) / 1000f}");
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