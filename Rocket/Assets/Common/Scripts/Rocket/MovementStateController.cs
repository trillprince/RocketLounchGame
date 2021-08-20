using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.Rocket;
using UnityEngine;

public  class MovementStateController : MonoBehaviour
{
    private IMoveComponent _movementComponent;
    private MovementResult _movementResult;
    private MovementState _movementState = MovementState.NoMovement;
    private Transform _startTransform;
    
    public static event Action <MovementState> OnMovementStateSwitch;

    private void OnEnable()
    {
        GameController.OnStateSwitch += OnOnStateSwitch;
    }

    private void OnDisable()
    {
        GameController.OnStateSwitch -= OnOnStateSwitch;
    }

    private void Start()
    {
        _startTransform = transform;
    }

    private void FixedUpdate()
    {
        _movementComponent?.Move(ChangeMovementComponent);
    }
    
    private void ChangeMovementComponent(MovementState movementResult)
    {
        switch (movementResult)
        {
            case MovementState.AutoMovement:
                _movementComponent = new RocketAutoMovement(transform,_startTransform);
                break;
            case MovementState.PhysicMovement:
                _movementComponent = new RocketLandingMove(transform,GetComponent<Rigidbody>(), ChangeMovementResult);
                break;
            case MovementState.NoMovement:
                _movementComponent = null;
                break;
        }
    }

    private void ChangeMovementResult(MovementResult movementResult)
    {
        this._movementResult = movementResult;
        //TODO: if success/fail -> end game
    }


    private void OnOnStateSwitch(GameState state)
    {
        if (state == GameState.CargoDrop)
        {
            OnMovementStateSwitch?.Invoke(MovementState.AutoMovement);
            ChangeMovementComponent(MovementState.AutoMovement);
        }
        else if (state == GameState.Landing)
        {
            OnMovementStateSwitch?.Invoke(MovementState.PhysicMovement);
            ChangeMovementComponent(MovementState.PhysicMovement);
        }
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
