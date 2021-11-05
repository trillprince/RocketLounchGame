using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common.Scripts.Input
{
    public class InputManager: IEnablable
    {
        private IInputPlatform _inputPlatform;
        public event Action<Vector3> OnInput;
        public event Action OnTouch;
        
        public InputManager(IInputPlatform inputPlatform)
        {
            _inputPlatform = inputPlatform;
        }
        
        private void OnInputListener(Vector3 inputContext)
        {
            OnInput?.Invoke(inputContext);
        }

        private void OnTouchListener()
        {
            OnTouch?.Invoke();
        }

        public void Enable()
        {
            _inputPlatform.Enable();
            _inputPlatform.OnInput += OnInputListener;
            _inputPlatform.OnTouch += OnTouchListener;
        }

        public void Disable()
        {
            _inputPlatform.Disable();
            _inputPlatform.OnInput -= OnInputListener;
            _inputPlatform.OnTouch -= OnTouchListener;
        }
    }
}