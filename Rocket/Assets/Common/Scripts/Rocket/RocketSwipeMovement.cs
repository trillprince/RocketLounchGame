using System;
using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Input;
using Common.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketSwipeMovement : IRocketMoveComponent
    {
        private readonly Transform _transform;
        private readonly SwipeDetection _swipeDetection;
        private readonly Vector3 _screenBoundaries;
        private int _currentPositionIndex = 0;
        private readonly Vector3 _leftPosition;
        private readonly Vector3 _rightPosition;
        private readonly Vector3 _middlePosition;
        private readonly MeshCollider _meshCollider;

        public RocketSwipeMovement(Transform transform,SwipeDetection swipeDetection, Vector3 screenBoundaries)
        {
            _transform = transform;
            _meshCollider = _transform.GetComponent<MeshCollider>();
            _swipeDetection = swipeDetection;
            _screenBoundaries = screenBoundaries;
            
            _leftPosition = new Vector3(
                (screenBoundaries.x - _transform.position.x) / 2,
                -screenBoundaries.y + _meshCollider.bounds.size.y / 2,
                _transform.position.z);
            
            _rightPosition = new Vector3(
                (- _screenBoundaries.x + _transform.position.x) / 2,
                -_screenBoundaries.y + _meshCollider.bounds.size.y / 2,
                _transform.position.z);
            
            _middlePosition = new Vector3(
                (_transform.position.x) / 2,
                -_screenBoundaries.y + _meshCollider.bounds.size.y / 2,
                _transform.position.z);
        }

        public void Move(Action<MovementState> changeState = null)
        {
            
        }

        public void Enable()
        {
            _swipeDetection.OnSwipeLeft += MoveOnLeftSwipe;
            _swipeDetection.OnSwipeRight += MoveOnRightSwipe;
        }

        private void MoveOnRightSwipe()
        {
            if (_currentPositionIndex < 0)
            {
                _currentPositionIndex++;
            }
            else if (_currentPositionIndex == 0 )
            {
                _currentPositionIndex++;
            }

        }
        
        

        private void MoveOnLeftSwipe()
        {
            
        }

        public void Disable()
        {
            
        }
    }
}