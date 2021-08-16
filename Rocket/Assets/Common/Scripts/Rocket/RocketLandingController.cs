using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RocketLandingController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _curFuel;
    [SerializeField] private float _thrustForce;
    private bool _isHeld;
    private float _fuelReduceSpeed = 1000;
    private Vector2 _touchPos;

    private void Awake()
    {
        _curFuel = _maxFuel;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.OnTouchHoldEvent += TouchHeld;
        RocketControl.Landing += RocketControlOnLanding;
    }

    private void RocketControlOnLanding()
    {
        _rb.isKinematic = false;
    }

    private void OnDisable()
    {
        InputManager.OnTouchHoldEvent -= TouchHeld;
    }

    private void Update()
    {
        if (_isHeld)
        {
            Flying();
        }
    }

    void Flying()
    {
        int touchPart = 0;
        if (_touchPos.x < Screen.width / 2)
        {
            touchPart = 1;
        }
        else if (_touchPos.x > Screen.width / 2)
        {
            touchPart = -1;
        }
        Vector3 moveTo = new Vector3(touchPart, transform.up.y);
        _rb.AddForce(moveTo * _thrustForce, ForceMode.Impulse);
    }

    void TouchHeld(Vector2 touchPos, bool isHeld)
    {
        _isHeld = isHeld;
        _touchPos = touchPos;
    }
}