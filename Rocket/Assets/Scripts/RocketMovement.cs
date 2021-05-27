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
    private float _rotateSpeed = 40f;
    private float _rotateMaxSpeed = 70f;
    private float speedAcceleration = 400f;
    private float speedDegradation = 150f;
    private bool _rocketLounched = false;
    private bool _touchPressing = false;
    [SerializeField] [Range(0,50f)] private float _rocketSpeed = 0;
    private float _rocketMaxSpeed = 50f;
    [SerializeField] private bool _middleEngineEnabled = false;

    public float RocketSpeed
    {
        get => _rocketSpeed;
        set => _rocketSpeed = value;
    }

    public void MiddleEngineEnabled(bool isEnabled)
    {
        _middleEngineEnabled = isEnabled;
    }

    public void LounchRocket()
    {
        _rocketLounched = true;
        MiddleEngineEnabled(true);
    }

    private void MiddleEngineSpeed()
    {
        if (!_middleEngineEnabled && _rocketSpeed > 0)
        {
            _rocketSpeed -= Time.deltaTime * 10;
        }
        else if (!_middleEngineEnabled && _rocketSpeed < 0)
        {
            _rocketSpeed = 0;
        }
        
        if (_middleEngineEnabled && _rocketSpeed < _rocketMaxSpeed)
        {
            _rocketSpeed += Time.deltaTime * 10;
        }
        else if (_rocketSpeed > _rocketMaxSpeed)
        {
            _rocketSpeed = _rocketMaxSpeed;
        }
        
    }
    private void Awake()
    {
        MiddleEngineEnabled(false);
        _touchControls = new TouchControls();
        _touchControls.Touch.TouchHold.performed += context => { _touchPressing = true; };
        _touchControls.Touch.TouchHold.canceled += context => { _touchPressing = false; };
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        LounchManager.RocketLounch += LounchRocket;
    }

    private void OnDisable()
    {
        _touchControls.Disable();
        LounchManager.RocketLounch -= LounchRocket;
    }


    public void Update()
    {
        if (!_rocketLounched)
        {
           return;
        }
        MiddleEngineSpeed();
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