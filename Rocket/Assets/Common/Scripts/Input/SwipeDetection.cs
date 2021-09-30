using System;
using System.Net.NetworkInformation;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class SwipeDetection: IEnablable
    {
        private readonly InputManager _inputManager;
        private Vector2 _startTouchPos;
        private float _startTime;
        private float _minDistance = 0.4f;
        private float _maxTime = 1f;
        private float _directionThreshhold = 0.9f;
        private UnityEngine.Camera _camera;
        public event Action OnSwipeLeft;
        public event Action OnSwipeRight;
        public event Action OnSwipeUp;
        public event Action OnSwipeDown;

        public event Action OnSwipeEnd; 




        public SwipeDetection(InputManager inputManager)
        {
            _inputManager = inputManager;
            _camera = UnityEngine.Camera.main;
        }
        
        public void Enable()
        {
            _inputManager.OnTouchStartEvent += SwipeStart;
            _inputManager.OnTouchEndEvent += SwipeEnd;
        }
        
        public void Disable()
        {
            _inputManager.OnTouchStartEvent -= SwipeStart;
            _inputManager.OnTouchEndEvent -= SwipeEnd;
        }

        
        private void SwipeStart(Vector2 touchPos, float touchStartTime)
        {
            _startTouchPos = touchPos;
            _startTime = touchStartTime;
        }

        private void SwipeEnd(Vector2 endTouchPos, float endTouchTime)
        {
            DetectSwipe(endTouchPos,endTouchTime);
            OnSwipeEnd?.Invoke();
        }

        private void DetectSwipe(Vector2 endTouchPos, float endTouchTime)
        {
            if (Vector3.Distance(_startTouchPos, endTouchPos) >= _minDistance &&
                (endTouchTime - _startTime) <= _maxTime)
            {
                SwipeDirection(endTouchPos);
            }
        }

        private void SwipeDirection(Vector2 endTouchPos)
        {
            Vector3 direction = (endTouchPos - _startTouchPos).normalized;
            Vector2 direction2D = new Vector2(direction.x, direction.y);

            if (Vector2.Dot(Vector2.up, direction) > _directionThreshhold)
            {
                OnSwipeUp?.Invoke();
            }
            else if (Vector2.Dot(Vector2.down, direction) > _directionThreshhold)
            {
                OnSwipeDown?.Invoke();
            }
            else if (Vector2.Dot(Vector2.left, direction) > _directionThreshhold)
            {
                OnSwipeLeft?.Invoke();
            }
            else if (Vector2.Dot(Vector2.right, direction) > _directionThreshhold)
            {
                OnSwipeRight?.Invoke();
            }
            
            
        }
    }
}