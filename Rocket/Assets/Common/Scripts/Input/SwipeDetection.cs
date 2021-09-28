using System.Net.NetworkInformation;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class SwipeDetection: IEnablable
    {
        private readonly InputManager _inputManager;
        private readonly Trail _trail;
        private Vector2 _startTouchPos;
        private float _startTime;
        private float _minDistance = 0.2f;
        private float _maxTime = 1f;
        private float _directionThreshhold = 0.9f;
        private UnityEngine.Camera _camera;

        public SwipeDetection(InputManager inputManager,GameObject trail)
        {
            _inputManager = inputManager;
            _trail = new Trail(inputManager,trail);
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
            _trail.Enable(Utils.ScreenToWorld(_camera,touchPos));
        }

        private void SwipeEnd(Vector2 endTouchPos, float endTouchTime)
        {
            DetectSwipe(endTouchPos,endTouchTime);
            _trail.Disable();

        }

        private void DetectSwipe(Vector2 endTouchPos, float endTouchTime)
        {
            if (Vector3.Distance(_startTouchPos, endTouchPos) >= _minDistance &&
                (endTouchTime - _startTime) <= _maxTime)
            {
                Debug.DrawLine(_startTouchPos,endTouchPos, Color.red,5f);
                SwipeDirection(endTouchPos);
            }
        }

        private void SwipeDirection(Vector2 endTouchPos)
        {
            Vector3 direction = (endTouchPos - _startTouchPos).normalized;
            Vector2 direction2D = new Vector2(direction.x, direction.y);

            if (Vector2.Dot(Vector2.up, direction) > _directionThreshhold)
            {
                Debug.Log("swipe up");
            }
            else if (Vector2.Dot(Vector2.down, direction) > _directionThreshhold)
            {
                Debug.Log("swipe down");
                
            }
            else if (Vector2.Dot(Vector2.left, direction) > _directionThreshhold)
            {
                Debug.Log("swipe left");
                
            }
            else if (Vector2.Dot(Vector2.right, direction) > _directionThreshhold)
            {
                Debug.Log("swipe right");
                
            }
            
            
        }
    }
}