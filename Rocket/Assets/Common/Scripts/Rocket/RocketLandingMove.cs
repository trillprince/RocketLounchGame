using System;
using Common.Scripts.Input;
using UnityEngine;

public class RocketLandingMove : IMoveComponent
{
    private Rigidbody _rb;
    private float _thrustForce = 15;
    private int _destroySpeed = 7;
    private bool _landingDone;
    private readonly float _maxRayDistance = 0.3f;
    private bool _touchHold;
    private Vector2 _touchPos;
    private Transform _transform;
    private BoundariesCheck _boundariesCheck;
    private Action<MovementResult> _changeMovementResult;
    private Action<Action<Vector2>, Action> _onInputSubscribe;
    private Action<Action<Vector2>, Action> _onInputUnsubscribe;

    public RocketLandingMove(Transform transform, Rigidbody rigidbody,
        Action<MovementResult> changeMovementResult,
        Action<Action<Vector2>, Action> onInputSubscribe,
        Action<Action<Vector2>, Action> onInputUnsubscribe)
    {
        _rb = rigidbody;
        _transform = transform;
        _changeMovementResult = changeMovementResult;
        _onInputSubscribe = onInputSubscribe;
        _onInputUnsubscribe = onInputUnsubscribe;
        _onInputSubscribe?.Invoke(OnTouchHold, OnTouchHoldEnd);
        _boundariesCheck = new BoundariesCheck(_rb, _rb.GetComponentInChildren<MeshCollider>(), Camera.main);
        PhysicActive(true);
    }

    ~RocketLandingMove()
    {
        _onInputUnsubscribe?.Invoke(OnTouchHold, OnTouchHoldEnd);
    }

    private void OnTouchHoldEnd()
    {
        _touchHold = false;
    }

    private void OnTouchHold(Vector2 touchPos)
    {
        _touchPos = touchPos;
        _touchHold = true;
    }

    private void PhysicActive(bool isActive)
    {
        _rb.isKinematic = !isActive;
    }

    void Flying(Vector2 touchPos)
    {
        int touchPart = 0;
        if (touchPos.x < Screen.width / 2)
        {
            touchPart = 1;
        }
        else
        {
            touchPart = -1;
        }

        Vector3 moveToVec = new Vector3(touchPart, 0.8f);
        _rb.AddForce(moveToVec * _thrustForce, ForceMode.Impulse);
    }

    void LandingCheck(Action<MovementState> changeState)
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, -_transform.up, out hit, _maxRayDistance))
        {
            bool onPad = hit.collider.CompareTag("LounchPad");
            bool crashing = _rb.velocity.magnitude >= _destroySpeed;
            if (onPad && !crashing)
            {
                changeState?.Invoke(MovementState.NoMovement);
                _changeMovementResult.Invoke(MovementResult.Successful);
            }

            if (hit.collider != null)
            {
                changeState?.Invoke(MovementState.NoMovement);
                _changeMovementResult.Invoke(MovementResult.Failed);
            }
        }
    }

    public void Move(Action<MovementState> changeState)
    {
        _boundariesCheck.OnScreenBoundaries((() =>
        {
            if (_touchHold)
            {
                Flying(_touchPos);
            }
        }),_thrustForce);
        LandingCheck(changeState);
    }
}