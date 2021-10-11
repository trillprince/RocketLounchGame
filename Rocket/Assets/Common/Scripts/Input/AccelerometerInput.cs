﻿using System;
using UnityEngine.InputSystem;

namespace Common.Scripts.Input
{
    public class AccelerometerInput : IInputPlatform
    {
        private readonly TouchControls _touchControls;

        public AccelerometerInput(TouchControls touchControls)
        {
            _touchControls = touchControls;
        }

        public void SubscribeToInput(Action<InputAction.CallbackContext> onInput)
        {
            _touchControls.Accelerometer.Acceleration.performed += onInput;
        }
        
        public void UnsubscribeFromInput(Action<InputAction.CallbackContext> onInput)
        {
            _touchControls.Accelerometer.Acceleration.performed -= onInput;
        }

        public void Enable()
        {
            _touchControls.Enable();
            InputSystem.EnableDevice(Accelerometer.current);
        }

        public void Disable()
        {
            _touchControls.Disable();
            InputSystem.DisableDevice(Accelerometer.current);
        }
    }
}