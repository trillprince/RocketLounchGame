using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.Rocket;
using UnityEngine;

public  class MovementStateController : MonoBehaviour
{
    private static bool _rocketAutoMove;

    private IMoveComponent _movementComponent;
    private MovementResult movementResult;

    public static event Action <MovementState> OnMovementStateSwitch;
    public static bool RocketAutoMove => _rocketAutoMove;

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

    private void Update()
    {
        _movementComponent?.Move(transform, ChangeMovementComponent);
    }
    
    private void ChangeMovementComponent(MovementState movementResult)
    {
        switch (movementResult)
        {
            case MovementState.AutoMovement:
                break;
            case MovementState.PhysicMovement:
                _movementComponent = new RocketLandingMove(GetComponent<Rigidbody>(), changeMovementResult);
                break;
            case MovementState.NoMovement:
                _movementComponent = null;
                break;
        }
    }

    private void changeMovementResult(MovementResult movementResult)
    {
        this.movementResult = movementResult;
        //TODO: if success/fail -> end game
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

}

public enum MovementState
{
    NoMovement,
    AutoMovement,
    PhysicMovement
}

public enum MovementResult
{
    Successful,
    Failed
}
