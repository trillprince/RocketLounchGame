using System;
using Common.Scripts.Input;
using UnityEngine.InputSystem;

public class EditorInput : IInputPlatform
{
    private readonly TouchControls _touchControls;
    

    public EditorInput(TouchControls touchControls)
    {
        _touchControls = touchControls;
    }

    public void Enable()
    {
        _touchControls.Enable();
    }

    public void Disable()
    {
        _touchControls.Disable();
    }

    public void SubscribeToInput(Action<InputAction.CallbackContext> onInput)
    {
        _touchControls.Arrows.ArrowPress.performed += onInput;
    }

    public void UnsubscribeFromInput(Action<InputAction.CallbackContext> onInput)
    {
        _touchControls.Arrows.ArrowPress.performed -= onInput;
    }
}