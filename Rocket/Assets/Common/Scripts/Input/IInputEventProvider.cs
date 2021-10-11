using System;
using UnityEngine.InputSystem;

namespace Common.Scripts.Input
{
    public interface IInputEventProvider
    {
        void SubscribeToInput(Action<InputAction.CallbackContext> onInput);

        void UnsubscribeFromInput(Action<InputAction.CallbackContext> onInput);
    }
}