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
        private readonly MeshCollider _meshCollider;
        private bool _swipeActive;
        private readonly Vector3 _middlePosition;

        public RocketSwipeMovement(Transform transform, SwipeDetection swipeDetection, Vector3 screenBoundaries)
        {
            _transform = transform;
            _meshCollider = _transform.GetComponentInChildren<MeshCollider>();
            _swipeDetection = swipeDetection;
            _screenBoundaries = screenBoundaries;

            _leftPosition = new Vector3(
                (screenBoundaries.x - _transform.position.x) / 2 + _meshCollider.bounds.size.x,
                _transform.position.y,
                _transform.position.z);

            _rightPosition = new Vector3(
                (-_screenBoundaries.x + _transform.position.x) / 2 - _meshCollider.bounds.size.x,
                _transform.position.y,
                _transform.position.z);

            _middlePosition = new Vector3(
                (_transform.position.x),
                _transform.position.y,
                _transform.position.z);
        }

        public void Move(Action<MovementState> changeState = null)
        {
            
        }

        public void Enable()
        {
            _swipeDetection.OnSwipeLeft += ChangeIndexOnLeftSwipe;
            _swipeDetection.OnSwipeRight += ChangeIndexOnRightSwipe;
        }

        private void ChangeIndexOnRightSwipe()
        {
            
            if (_currentPositionIndex == -1)
            {
                _currentPositionIndex++;
                Move(_middlePosition);
            }
            else if (_currentPositionIndex == 0)
            {
                _currentPositionIndex++;
                Move(_rightPosition);
            }
        }

        private void ChangeIndexOnLeftSwipe()
        {
            if (_currentPositionIndex == 1)
            {
                _currentPositionIndex--;
                Move(_middlePosition);
            }
            else if (_currentPositionIndex == 0)
            {
                _currentPositionIndex--;
                Move(_leftPosition);
            }
        }

        private void Move(Vector3 to)
        {
            _transform.position = to;
        }

        public void Disable()
        {
            _swipeDetection.OnSwipeLeft -= ChangeIndexOnLeftSwipe;
            _swipeDetection.OnSwipeRight -= ChangeIndexOnRightSwipe;
        }
    }
}