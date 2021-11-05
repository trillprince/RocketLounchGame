using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.Input
{
    public class AndroidInput : IInputPlatform
    {
        private readonly TouchControls _touchControls;
        public event Action <Vector3> OnInput;
        public event Action  OnTouch;
        
        public AndroidInput(TouchControls touchControls)
        {
            _touchControls = touchControls;
        }

        public void Enable()
        {
            _touchControls.Enable();
            _touchControls.AndroidInput.Acceleration.performed += OnInputPerformed;
            _touchControls.AndroidInput.Touch.performed += OnTouchPerformed;
        }

        private void OnTouchPerformed(InputAction.CallbackContext context)
        {
            OnTouch?.Invoke();
        }

        public void Disable()
        {
            _touchControls.Disable();
            _touchControls.AndroidInput.Acceleration.performed -= OnInputPerformed;
            _touchControls.AndroidInput.Touch.performed -= OnTouchPerformed;
        }
        
        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            OnInput?.Invoke(context.ReadValue<Vector3>());
        }
        
    }
}