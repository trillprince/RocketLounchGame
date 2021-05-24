using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour
{
    private TouchControls _touchControls;
    private Coroutine _movementProcess;
    private float _moveSpeed = 5f;
    private float _rotateSpeed = 3f;
    private bool _rocketLounched = false;
    private int _sideToRotate;

    public bool RocketLounched
    {
        get => _rocketLounched;
    }

    private void OnEnable()
    {
        _touchControls.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    private void Awake()
    {
        _touchControls = new TouchControls();
        _touchControls.Touch.TouchHold.performed += context =>
        {
            MoveAndRotateToScreenSide(context);
        };
    }


    void MoveAndRotateToScreenSide(InputAction.CallbackContext context)
    {
        if (GetPositionOfTouch().x < Screen.width / 2)
        {
            _sideToRotate = 1;
            StartCoroutine(TransformWithDuration(-transform.right, context));
        }
        else if (GetPositionOfTouch().x > Screen.width / 2)
        {
            _sideToRotate = -1;
            StartCoroutine(TransformWithDuration(transform.right, context));
        }
    }

    Vector2 GetPositionOfTouch()
    {
        return _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }

    private IEnumerator TransformWithDuration(Vector2 sideToMove, InputAction.CallbackContext context)
    {
        while (context.phase == InputActionPhase.Performed)
        {
            Move(sideToMove);
            Rotate();
            yield return null;
        }
    }

    private void Move(Vector3 sideToMove)
    {
        transform.Translate(sideToMove * _moveSpeed * Time.deltaTime, Space.Self);
    }

    private void Rotate()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x , transform.localRotation.y , _sideToRotate * -_rotateSpeed * Time.deltaTime);
    }
}