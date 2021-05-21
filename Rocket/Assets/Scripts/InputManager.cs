using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leftTapText;
    [SerializeField] private TextMeshProUGUI _rightTapText;
    private TouchControls _touchControls;

    private void OnEnable()
    {
        _touchControls.Enable();
    }
    private void OnDisable()
    {
        _touchControls.Disable();
    }

    private void Awake()
    {
        _touchControls = new TouchControls();
    }
    
    void Start()
    {
        _touchControls.Touch.TouchHold.started += context => StartTouch(context);
        _touchControls.Touch.TouchHold.canceled += context => EndTouch(context);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touch started" + _touchControls.Touch.TouchPosition.ReadValue<Vector3>());
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touch ended" + _touchControls.Touch.TouchPosition.ReadValue<Vector3>());
    }

   
    
    
}
