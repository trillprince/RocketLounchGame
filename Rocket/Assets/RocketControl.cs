using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    private float _rocketSpeed = 40f;
    private float _rocketMaxSpeed = 100f;
    private float _rocketSpeedAcceleration = 15f;
    private bool _rocketMove = false;
    public float RocketSpeed => _rocketSpeed;

    public static event Action <bool> RocketMoving;
    public static event Action Landing;

    private void OnEnable()
    {
        LounchManager.OnRocketLounch += IsMoving;
        GameController.OnStateSwitch += OnOnStateSwitch;
    }

    private void OnDisable()
    {
        LounchManager.OnRocketLounch -= IsMoving;
    }

    private void OnOnStateSwitch(GameState state)
    {
        if (state == GameState.Landing)
        {
            IsMoving(false);
            Landing?.Invoke();
        }
    }

    void Update()
    {
        if (_rocketMove)
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

    void IsMoving (bool isMoving)
    {
        _rocketMove = isMoving;
        RocketMoving?.Invoke(isMoving);
    }

    public void ChangeMovementType()
    {
        
    }
}
