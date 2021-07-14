using System;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls _touchControls;
        private bool _isTouching;

        public delegate void InputStart(Vector2 touchPosition);

        public static event InputStart TouchStart;
        
        public delegate void InputEnd();

        public static event InputEnd TouchEnd;

        private void Awake()
        {
            _touchControls = new TouchControls();
            _touchControls.Touch.TouchHold.performed += context => { TouchStart?.Invoke(GetPositionOfTouch()); };
            _touchControls.Touch.TouchHold.canceled += context => { TouchEnd?.Invoke();};
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
