using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.Rocket;
using UnityEngine;

public class MovementStateController : MonoBehaviour
{
    private IMoveComponent _movementComponent;
    private IMovementTransition _movementTransition;
    private MovementResult _movementResult;
    private MovementState _movementState = MovementState.NoMovement;
    private Transform _startTransform;
    private event Action <Transform,MovementState>  OnMovementStateSwitch;

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
        OnMovementStateSwitch?.Invoke(transform,movementResult);
        switch (movementResult)
        {
            case MovementState.AutoMovement:
                _movementComponent = new RocketAutoMovement(transform);
                _movementTransition = new TransitionToLanding(transform, _startTransform, movementResult,
                    OnMovementStateChangeSubscribe, OnMovementStateChangeUnsubscribe);
                break;
            case MovementState.PhysicMovement:
                _movementComponent = new RocketLandingMove(transform,
                    GetComponent<Rigidbody>(), 
                    ChangeMovementResult);
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
            ChangeMovementComponent(MovementState.AutoMovement);
        }
        else if (state == GameState.Landing)
        {
            ChangeMovementComponent(MovementState.PhysicMovement);
        }
    }

    private void OnMovementStateChangeSubscribe(Action<Transform, MovementState> action)
    {
        OnMovementStateSwitch += action;
    }
    private void OnMovementStateChangeUnsubscribe(Action<Transform, MovementState> action)
    {
        OnMovementStateSwitch -= action;
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
