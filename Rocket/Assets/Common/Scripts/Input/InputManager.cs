using System;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour,IControllable
    {
        private TouchControls _touchControls;

        public static event Action<Vector2> OnTouchStart;
        public static event Action OnTouchEnd;
        
        

        private void Awake()
        {
            _touchControls = new TouchControls();
            _touchControls.Touch.TouchHold.started += context => 
            {
                OnTouchStart?.Invoke(GetPositionOfTouch());
            };
                
            _touchControls.Touch.TouchHold.canceled += context =>
            { 
                OnTouchEnd?.Invoke();
            };
        }

   
        private void OnEnable()
        {
            _touchControls.Enable();
        }

        private void OnDisable()
        {
            _touchControls.Disable();
        }
        
        Vector2 GetPositionOfTouch()
        {
            return _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }
    }
}
