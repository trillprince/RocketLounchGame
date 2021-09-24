using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketAutoMovement : IRocketMoveComponent
    {
        private readonly Transform _transform;
        private float _timeTillValueReset = 5;
        private float _curremtTime;
        private Quaternion _targetRotation;
        private float _rotateDuration = 10;
        private float _rotateTimeElapsed;

        public RocketAutoMovement(Transform transform)
        {
            _transform = transform;
        }

        public void Move(Action<MovementState> changeState = null)
        {
            if (_curremtTime < _timeTillValueReset)
            {
                _curremtTime += Time.deltaTime;
                return;
            }
            Rotate();
            Scale();
        }

        private void Scale()
        {
        }

        private void Rotate()
        {
            _transform.Rotate(_transform.rotation.x,Vector3.right.y * Time.deltaTime,_transform.rotation.z);
        }

        public void Enable()
        {
            
        }
    }
}