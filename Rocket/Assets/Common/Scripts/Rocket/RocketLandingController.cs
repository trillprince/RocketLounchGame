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

    private void Awake()
    {
        _curFuel = _maxFuel;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.OnTouchHoldEvent += (vector2, b) => TouchHeld(b);
    }
    private void OnDisable()
    {
        InputManager.OnTouchHoldEvent -= (vector2, b) => TouchHeld(b);
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
        if (_curFuel > 0)
        {
            _curFuel -= Time.deltaTime;
            _rb.AddForce(_rb.transform.up * _thrustForce, ForceMode.Impulse);
        }
    }
    
    void TouchHeld(bool isHeld)
    {
        _isHeld = isHeld;
    }
}