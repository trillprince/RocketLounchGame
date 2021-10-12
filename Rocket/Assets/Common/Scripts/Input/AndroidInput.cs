using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.Input
{
    public class AndroidInput : IInputPlatform
    {
        private readonly TouchControls _touchControls;
        public event Action <Vector3> OnInput;
        
        public AndroidInput(TouchControls touchControls)
        {
            _touchControls = touchControls;
        }

        public void Enable()
        {
            _touchControls.Enable();
            _touchControls.AndroidInput.Acceleration.performed += OnInputPerformed;
        }

        public void Disable()
        {
            _touchControls.Disable();
            _touchControls.AndroidInput.Acceleration.performed -= OnInputPerformed;
        }
        
        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            OnInput?.Invoke(context.ReadValue<Vector3>());
        }
        
    }
}