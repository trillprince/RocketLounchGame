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

    private bool touchHold;
    private Vector2 touchPos;

    private Action<MovementResult> changeMovementResult;

    public RocketLandingMove(Rigidbody rigidbody, Action<MovementResult> changeMovementResult)
    {
        _rb = rigidbody;
        ActivatePhysic();
        this.changeMovementResult = changeMovementResult;
        InputManager.OnTouchHold += touchPos =>
        {
            this.touchPos = touchPos;
            this.touchHold = true;
        };
        InputManager.OnTouchHoldEnd += () =>
        {
            this.touchHold = false;
        };
    }

    private void ActivatePhysic()
    {
        _rb.isKinematic = false;
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

    void LandingCheck(Transform transform , Action <MovementState> changeState)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, _maxRayDistance))
        {
            bool onPad = hit.collider.CompareTag("LounchPad");
            bool crashing = _rb.velocity.magnitude >= _destroySpeed;
            if (onPad && !crashing)
            {
                changeState?.Invoke(MovementState.NoMovement);
                changeMovementResult.Invoke(MovementResult.Successful);
            }
            if (hit.collider != null)
            {
                changeState?.Invoke(MovementState.NoMovement);
                changeMovementResult.Invoke(MovementResult.Failed);
            }
        }
    }

    public void Move(Transform transform, Action<MovementState> changeState)
    {
        if (touchHold)
        {
            Flying(touchPos);
        }
        LandingCheck(transform, changeState);
    }
}

