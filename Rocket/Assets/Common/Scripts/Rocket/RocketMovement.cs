using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketMovement: IGameStateSubscriber
    {
        private readonly Transform _transform;
        private readonly MeshCollider _meshCollider;
        private readonly RocketSpeed _rocketSpeed;
        private IRocketMoveComponent _movementComponent;
        private Dictionary<Type, IRocketMoveComponent> _movementComponents;
        private IMovementTransition _movementTransition;
        private Vector3 _screenBounds;
        public Rigidbody Rigidbody { get; private set; }
        private event Action<Transform, MovementState> OnMovementStateSwitch;
        public static event Action<LandingStatus> OnLanding;

        public RocketMovement(InputManager inputManager,Rigidbody rigidbody,
            Transform transform, MeshCollider meshCollider,RocketSpeed rocketSpeed)
        {
            _transform = transform;
            _meshCollider = meshCollider;
            _rocketSpeed = rocketSpeed;
            Rigidbody = rigidbody;
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - Rigidbody.position.z));
            
            _movementTransition = new TransitionToLanding(transform, MovementState.PhysicMovement,
                OnMovementStateChangeSubscribe, OnMovementStateChangeUnsubscribe);

            _movementComponents = new Dictionary<Type, IRocketMoveComponent>
            {
                [typeof(RocketAccelerationMovement)] = new RocketAccelerationMovement(this,transform,inputManager,_screenBounds),
                [typeof(RocketLandingRocketMove)] = new RocketLandingRocketMove(transform,
                    Rigidbody,
                    OnGetLandingStatus, _screenBounds)
            };
        }

        public MeshCollider GetMeshCollider()
        {
            return _meshCollider;
        }

        public Vector3 GetRocketDirection()
        {
            return RocketSpeed.RocketDirection;
        }

        public float GetRocketSpeed()
        {
            return _rocketSpeed.CurrentSpeed;
        }

        public Transform GetTransform()
        {
            return _transform;
        }
        

        private void ChangeMovementComponent(MovementState movementResult)
        {
            switch (movementResult)
            {
                case MovementState.SwipeMovement:
                    _movementComponent = _movementComponents[typeof(RocketAccelerationMovement)];
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
            OnMovementStateSwitch?.Invoke(_transform, movementResult);
        }

        private void OnGetLandingStatus(LandingStatus landingStatus)
        {
            OnLanding?.Invoke(landingStatus);
        }


        public void OnGameStateChange(GameState state)
        {
            if (state == GameState.CargoDrop)
            {
                ChangeMovementComponent(MovementState.SwipeMovement);
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
        SwipeMovement,
        PhysicMovement
    }

    public enum LandingStatus
    {
        Successful,
        Failed
    }
}