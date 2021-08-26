using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class MovementStateController : MonoBehaviour
    {
        private IMoveComponent _movementComponent;
        private Dictionary<Type, IMoveComponent> _movementComponents;
        private IMovementTransition _movementTransition;
        private MovementResult _movementResult;
        private MovementState _movementState = MovementState.NoMovement;
        private Transform _startTransform;
        private event Action<Transform, MovementState> OnMovementStateSwitch;

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
            _movementTransition = new TransitionToLanding(transform, MovementState.PhysicMovement,
                OnMovementStateChangeSubscribe, OnMovementStateChangeUnsubscribe);

            _movementComponents = new Dictionary<Type, IMoveComponent>
            {
                [typeof(RocketAutoMovement)] = new RocketAutoMovement(transform),
                [typeof(RocketLandingMove)] = new RocketLandingMove(transform,
                    GetComponent<Rigidbody>(),
                    ChangeMovementResult, OnInputSubscribe, OnInputUnsubscribe)
            };
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
                    _movementComponent = _movementComponents[typeof(RocketAutoMovement)];
                    _movementComponent.Enable();
                    break;
                case MovementState.PhysicMovement:
                    _movementComponent = _movementComponents[typeof(RocketLandingMove)];
                    _movementComponent.Enable();
                    break;
                case MovementState.NoMovement:
                    _movementComponent = null;
                    break;
            }

            OnMovementStateSwitch?.Invoke(transform, movementResult);
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

        private void OnInputSubscribe(Action<Vector2> touchHold, Action touchHoldEnd)
        {
            InputManager.OnTouchHold += touchHold;
            InputManager.OnTouchHoldEnd += touchHoldEnd;
        }

        private void OnInputUnsubscribe(Action<Vector2> touchHold, Action touchHoldEnd)
        {
            InputManager.OnTouchHold -= touchHold;
            InputManager.OnTouchHoldEnd -= touchHoldEnd;
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
}