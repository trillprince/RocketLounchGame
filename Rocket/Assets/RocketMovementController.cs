using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class RocketMovementController : MonoBehaviour
{
    private float _rocketSpeed = 40f;
    private float _rocketMaxSpeed = 100f;
    private float _rocketSpeedAcceleration = 15f;
    private bool _rocketLounched = false;

    public float RocketSpeed => _rocketSpeed;

    private void OnEnable()
    {
        LounchManager.OnRocketLounch += SetLounchStatus;
    }

    private void OnDisable()
    {
        LounchManager.OnRocketLounch -= SetLounchStatus;
    }

    void Update()
    {
        if (_rocketLounched)
        {
            SpeedCalculate();
        }
    }

    void SpeedCalculate()
    {
        if (RocketSpeed < _rocketMaxSpeed)
        {
            _rocketSpeed = RocketSpeed + Time.deltaTime * _rocketSpeedAcceleration;
        }
        else if (RocketSpeed > _rocketMaxSpeed)
        {
            _rocketSpeed = _rocketMaxSpeed;
        }
    }

    public Vector3 GetRocketDirection()
    {
        return transform.up;
    }

    void SetLounchStatus (bool isLounched)
    {
        _rocketLounched = isLounched;
    }

    public void ChangeMovementType()
    {
        
    }
}
