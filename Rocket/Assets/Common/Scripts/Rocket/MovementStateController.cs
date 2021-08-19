using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

public  class MovementStateController : MonoBehaviour
{
    private MovementState _movementState = MovementState.NoMovement;
    private IMoveState _iMoveState;
    private static bool _rocketAutoMove;
    public static event Action <MovementState> OnMovementStateSwitch;

    public static bool RocketAutoMove => _rocketAutoMove;

    public MovementState MovementState => _movementState;


    private void OnEnable()
    {
        GameController.OnStateSwitch += OnOnStateSwitch;
        LaunchManager.OnRocketLounch += OnLounch;
    }

    private void OnDisable()
    {
        GameController.OnStateSwitch -= OnOnStateSwitch;
        LaunchManager.OnRocketLounch -= OnLounch;
    }

    private void OnOnStateSwitch(GameState state)
    {
        if (state == GameState.Landing)
        {
            OnMovementStateSwitch?.Invoke(MovementState.PhysicMovement);
        }
    }
    
    void OnLounch (bool isMoving)
    {
        _rocketAutoMove = isMoving;
        OnMovementStateSwitch?.Invoke(MovementState.AutoMovement);
    }
    
    
    private void ChangeMovementType(MovementState movementState)
    {
        _movementState = movementState;
    }
}

public enum MovementState
{
    NoMovement,
    AutoMovement,
    PhysicMovement
}