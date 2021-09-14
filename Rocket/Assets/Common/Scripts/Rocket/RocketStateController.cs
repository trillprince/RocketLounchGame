using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketStateController : MonoBehaviour
    {
        private IRocketMoveComponent _movementComponent;
        private Dictionary<Type, IRocketMoveComponent> _movementComponents;
        private IMovementTransition _movementTransition;
        private event Action<Transform, MovementState> OnMovementStateSwitch;
        public static event Action<LandingStatus> OnLanding; 

        private void OnEnable()
        {
            GameStateController.OnStateSwitch += OnOnStateSwitch;
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= OnOnStateSwitch;
        }

        private void Start()
        {
            _movementTransition = new TransitionToLanding(transform, MovementState.PhysicMovement,
                OnMovementStateChangeSubscribe, OnMovementStateChangeUnsubscribe);

            _movementComponents = new Dictionary<Type, IRocketMoveComponent>
            {
                [typeof(RocketAutoMovement)] = new RocketAutoMovement(transform),
                [typeof(RocketLandingRocketMove)] = new RocketLandingRocketMove(transform,
                    GetComponent<Rigidbody>(),
                    OnGetLandingStatus, OnInputSubscribe, OnInputUnsubscribe)
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
                    _movementComponent = _movementComponents[typeof(RocketLandingRocketMove)];
                    _movementComponent.Enable();
                    break;
                case MovementState.NoMovement:
                    _movementComponent = null;
                    break;
            }
            OnMovementStateSwitch?.Invoke(transform, movementResult);
        }

        private void OnGetLandingStatus(LandingStatus landingStatus)
        {
            OnLanding?.Invoke(landingStatus);
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
            InputManager.OnTouchStart += touchHold;
            InputManager.OnTouchEnd += touchHoldEnd;
        }

        private void OnInputUnsubscribe(Action<Vector2> touchHold, Action touchHoldEnd)
        {
            InputManager.OnTouchStart -= touchHold;
            InputManager.OnTouchEnd -= touchHoldEnd;
        }
    }

    public enum MovementState
    {
        NoMovement,
        AutoMovement,
        PhysicMovement
    }

    public enum LandingStatus
    {
        Successful,
        Failed
    }
}