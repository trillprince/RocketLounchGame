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
    private float _rotateSpeed = 8f;
    private bool _rocketLounched = false;
    private int _sideToRotate;
    private BGScroll _background;

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
        _background = FindObjectOfType<BGScroll>();
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
            /*_background.MoveRightScroll();*/
            _sideToRotate = 1;
            StartCoroutine(TransformWithDuration(-transform.right, context));
        }
        else if (GetPositionOfTouch().x > Screen.width / 2)
        {
            /*_background.MoveLeftScroll();*/
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
            Rotate();
            yield return null;
        }
    }

    private void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, 0 ,_sideToRotate*_rotateSpeed*Time.deltaTime);
    }

    public Vector3 GetRocketDirection()
    {
        return transform.up;
    }
}