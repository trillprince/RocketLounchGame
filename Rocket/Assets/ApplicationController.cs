using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationController : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (hasFocus)
            {
                InputSystem.EnableDevice(Accelerometer.current);
                return;
            }
            InputSystem.DisableDevice(Accelerometer.current);
        }
    }
}
