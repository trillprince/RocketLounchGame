using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketMovementController : MonoBehaviour
    {
        private IRocketMoveComponent _movementComponent;
        private Dictionary<Type, IRocketMoveComponent> _movementComponents;
        private IMovementTransition _movementTransition;
        private SwipeDetection _swipeDetection;
        private Vector3 _screenBounds;
        public Rigidbody Rigidbody { get; private set; }
        private event Action<Transform, MovementState> OnMovementStateSwitch;
        public static event Action<LandingStatus> OnLanding;

        [Inject]
        public void Constructor(SwipeDetection swipeDetection)
        {
            _swipeDetection = swipeDetection;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - Rigidbody.position.z));
        }

        private void OnEnable()
        {
            GameStateController.OnStateSwitch += OnOnStateSwitch;
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= OnOnStateSwitch;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public Vector3 GetRocketDirection()
        {
            return RocketSpeed.RocketDirection;
        }

        public float GetRocketSpeed()
        {
            return RocketSpeed.CurrentSpeed;
        }

        public Transform GetTransform()
        {
            return transform;
        }
        
        private void Start()
        {
            _movementTransition = new TransitionToLanding(transform, MovementState.PhysicMovement,
                OnMovementStateChangeSubscribe, OnMovementStateChangeUnsubscribe);

            _movementComponents = new Dictionary<Type, IRocketMoveComponent>
            {
                [typeof(RocketSwipeMovement)] = new RocketSwipeMovement(transform,_swipeDetection,_screenBounds),
                [typeof(RocketLandingRocketMove)] = new RocketLandingRocketMove(transform,
                    Rigidbody,
                    OnGetLandingStatus, OnInputSubscribe, OnInputUnsubscribe,_screenBounds)
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
                    _movementComponent = _movementComponents[typeof(RocketSwipeMovement)];
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

        private void OnInputSubscribe(Action <Vector2> touchHold, Action <Vector2>  touchHoldEnd)
        {
            InputManager.OnTouchStart += touchHold;
            InputManager.OnTouchEnd += touchHoldEnd;
        }
        
        private void OnInputUnsubscribe(Action <Vector2> touchHold, Action <Vector2> touchHoldEnd)
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