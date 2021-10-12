using System;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private IInputPlatform _inputPlatform;
        public event Action<Vector3> OnInput;

        [Inject]
        public void Constructor(IInputPlatform inputPlatform)
        {
            _inputPlatform = inputPlatform;
        }
        
        private void OnInputListener(Vector3 inputContext)
        {
            OnInput?.Invoke(inputContext);
        }

        private void OnEnable()
        {
            _inputPlatform.Enable();
            _inputPlatform.OnInput += OnInputListener;
        }
        
        private void OnDisable()
        {
            _inputPlatform.Disable();
            _inputPlatform.OnInput -= OnInputListener;
        }
    }
}