using System;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls _touchControls;
        
        public delegate void InputStart(Vector2 touchPosition);

        public static event InputStart OnTouchStartEvent;
        
        public delegate void InputEnd();

        public static event InputEnd OnTouchEndEvent;

        public static event Action <Vector2> OnTouchHold;
        public static event Action  OnTouchHoldEnd;
        

        private void Awake()
        {
            _touchControls = new TouchControls();
            _touchControls.Touch.TouchHold.started += context => 
            {
                OnTouchStartEvent?.Invoke(GetPositionOfTouch());
                OnTouchHold?.Invoke(GetPositionOfTouch());
            };
                
            _touchControls.Touch.TouchHold.canceled += context =>
            { 
                OnTouchEndEvent?.Invoke();
                OnTouchHoldEnd?.Invoke();
            };
            
            Debug.Log("touch active");
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
    }
}
