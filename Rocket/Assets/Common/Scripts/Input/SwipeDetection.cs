using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class SwipeDetection: IEnablable
    {
        private readonly InputManager _inputManager;

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

 
        private void SwipeEnd(Vector2 arg1, float arg2)
        {

        }

        private void SwipeStart(Vector2 arg1, float arg2)
        {
            
        }
    }
}