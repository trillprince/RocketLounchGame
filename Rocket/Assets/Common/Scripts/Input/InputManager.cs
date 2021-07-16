using System;
using UnityEngine;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls _touchControls;
        private bool _controlEnable = false;

        public delegate void ControledInputStart(Vector2 touchPosition);

        public static event ControledInputStart OnTouchStartEvent;
        
        public delegate void ControledInputEnd();

        public static event ControledInputEnd OnTouchEndEvent;

        public delegate void UncontroledInput();

        public static event UncontroledInput StartEvent;
        public static event UncontroledInput EndEvent;

        private void Awake()
        {
            _touchControls = new TouchControls();
            if (_controlEnable)
            {
                _touchControls.Touch.TouchHold.performed += context => {
                    OnTouchStartEvent?.Invoke(GetPositionOfTouch());
                };
                _touchControls.Touch.TouchHold.canceled += context =>
                {
                    OnTouchEndEvent?.Invoke();
                };
            }
            else
            {
                _touchControls.Touch.TouchHold.started += context =>
                {
                    StartEvent?.Invoke();
                };
                _touchControls.Touch.TouchHold.canceled += context =>
                {
                    EndEvent?.Invoke();
                };
            }
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
