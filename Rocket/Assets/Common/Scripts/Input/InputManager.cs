using System;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        private IInputPlatform _inputPlatform;
        public event Action<Vector3> OnInput;


        [Inject]
        public void Constructor(IInputPlatform inputPlatform)
        {
            _inputPlatform = inputPlatform;
        }

        private void OnAccelerationPerformed(InputAction.CallbackContext context)
        {
            OnInput?.Invoke(context.ReadValue<Vector3>());
        }

        private void OnEnable()
        {
            _inputPlatform.Enable();
            _inputPlatform.SubscribeToInput(OnAccelerationPerformed);
        }


        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void OnDisable()
        {
            _inputPlatform.Disable();
            _inputPlatform.UnsubscribeFromInput(OnAccelerationPerformed);
        }
    }
}