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
        private float _minDistance = 0.2f;
        private float _maxTime = 1f;

        public SwipeDetection(InputManager inputManager)
        {
            _inputManager = inputManager;
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
        }

        private void DetectSwipe(Vector2 endTouchPos, float endTouchTime)
        {
            if (Vector3.Distance(_startTouchPos, endTouchPos) >= _minDistance &&
                (endTouchTime - _startTime) <= _maxTime)
            {
                Debug.DrawLine(_startTouchPos,endTouchPos, Color.red,5f);
            }
        }
    }
}