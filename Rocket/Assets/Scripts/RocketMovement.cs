using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour
{
    private TouchControls _touchControls;

    [SerializeField]
    private float _rotateSpeed = 0f;

    [SerializeField]
    private float _rotateMaxSpeed = 70f;

    [SerializeField]
    private float speedAcceleration = 400f;

    [SerializeField]
    private float speedDegradation = 150f;

    private bool _rocketLounched = false;

    [SerializeField]
    private bool _touchPressing = false;

    public void LounchRocket()
    {
        _rocketLounched = true;
    }


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

        _touchControls.Touch.TouchHold.performed += context => { _touchPressing = true; };
        _touchControls.Touch.TouchHold.canceled += context => { _touchPressing = false; };
    }

    public void Update()
    {
        if (!_rocketLounched)
        {
           return;
        }
        Rotate();
        if (_touchPressing)
        {
            MoveOnTouchScreen();
        }
        else
        {
            DegradateSpeed();
        } 
    }

    private void MoveOnTouchScreen()
    {
        if (GetPositionOfTouch().x < Screen.width / 2)
        {
            _rotateSpeed += Time.deltaTime * speedAcceleration;
        }
        else if (GetPositionOfTouch().x > Screen.width / 2)
        {
            _rotateSpeed -= Time.deltaTime * speedAcceleration;
        }

        if (_rotateSpeed >= _rotateMaxSpeed)
        {
            _rotateSpeed = _rotateMaxSpeed;
        }

        if (_rotateSpeed <= -_rotateMaxSpeed)
        {
            _rotateSpeed = -_rotateMaxSpeed;
        }
    }

    private void DegradateSpeed()
    {
        if (_rotateSpeed > 0)
        {
            if (_rotateSpeed - speedDegradation * Time.deltaTime < 0)
            {
                _rotateSpeed = 0;
            }
            else
            {
                _rotateSpeed -= speedDegradation * Time.deltaTime;
            }
        }
        else if (_rotateSpeed < 0)
        {
            if (_rotateSpeed + speedDegradation * Time.deltaTime > 0)
            {
                _rotateSpeed = 0;
            }
            else
            {
                _rotateSpeed += speedDegradation * Time.deltaTime;
            }
        }
    }


    Vector2 GetPositionOfTouch()
    {
        return _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }
    
    private void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, _rotateSpeed * Time.deltaTime);
    }

    public Vector3 GetRocketDirection()
    {
        return transform.up;
    }
}