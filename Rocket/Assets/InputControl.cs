using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputControl : MonoBehaviour
{
    private InputManager _inputManager;

    [Inject]
    public void Constructor(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void OnEnable()
    {
        _inputManager.Enable();
    }

    private void OnDisable()
    {
        _inputManager.Disable();
    }
}