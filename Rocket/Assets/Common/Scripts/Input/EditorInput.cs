using System;
using System.Collections;
using Common.Scripts.Infrastructure;
using Common.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorInput : IInputPlatform
{
    private readonly TouchControls _touchControls;
    private Coroutine _coroutine;
    public event Action <Vector3> OnInput;
    private ICoroutineRunner _coroutineRunner;
    private float _updateTime = 0.01f;

    public EditorInput(TouchControls touchControls, ICoroutineRunner coroutineRunner)
    {
        _touchControls = touchControls;
        _coroutineRunner = coroutineRunner;
    }

    public void Enable()
    {
        _touchControls.Enable();
        _touchControls.EditorInput.ArrowPress.started += OnInputPerformed;
    }

    public void Disable()
    {
        _touchControls.Disable();
        _touchControls.EditorInput.ArrowPress.started -= OnInputPerformed;
    }

    private void OnInputPerformed(InputAction.CallbackContext context)
    {
        if(_coroutine != null) return;
        _coroutine = _coroutineRunner.StartCoroutine(SimulateAcceleration(context));
    }

    private IEnumerator SimulateAcceleration(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        while (context.started && !context.canceled)
        {
            Vector3 inputDir = new Vector3(direction,0 , 0);
            OnInput?.Invoke(inputDir);
            yield return new WaitForSeconds(_updateTime);
        }
        _coroutine = null;
    }

}