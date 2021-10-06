using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls _touchControls;
        private UnityEngine.Camera _camera;
        public event Action<Vector3> OnAcceleration;

        [Inject]
        public void Constructor(TouchControls touchControls)
        {
            _touchControls = touchControls;
            _touchControls.Enable();
            InputSystem.EnableDevice(Accelerometer.current);
            _touchControls.Accelerometer.Acceleration.performed += OnAccelerationPerformed;
        }

        private void OnAccelerationPerformed(InputAction.CallbackContext context)
        {
            OnAcceleration?.Invoke(context.ReadValue<Vector3>());
        }


        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }
        
        private void OnDisable()
        {
            _touchControls.Disable();
            
        }

    }
}
