using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public class MovementTypeSwitcher : MonoBehaviour
{
    [SerializeField] private RocketSpeedStats _rocketSpeedStats;
    private bool _rocketMove = false;
    private float _currentSpeed;

    private MovementType _movementType = MovementType.AutoMovement;

    public static event Action <bool> RocketMoving;

    public static event Action Landing;

    public float CurrentSpeed
    {
        get => _currentSpeed ;
        set => _currentSpeed = value;
    }


    private void OnEnable()
    {
        LaunchManager.OnRocketLounch += IsMoving;
        GameController.OnStateSwitch += OnOnStateSwitch;
    }

    private void OnDisable()
    {
        LaunchManager.OnRocketLounch -= IsMoving;
    }

    private void OnOnStateSwitch(GameState state)
    {
        if (state == GameState.Landing)
        {
            IsMoving(false);
            Landing?.Invoke();
        }
    }

    private void Start()
    {
        CurrentSpeed = _rocketSpeedStats.RocketStartSpeed;
    }

    void Update()
    {
        if (_rocketMove)
        {
            CurrentSpeed = SpeedCalculator.CalculateSpeed(
                CurrentSpeed,
                _rocketSpeedStats.RocketMaxSpeed,
                _rocketSpeedStats.RocketSpeedAcceleration);
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

    private void ChangeMovementType(MovementType movementType)
    {
        _movementType = movementType;
    }
}

public enum MovementType
{
    AutoMovement,
    PhysicMovement
}