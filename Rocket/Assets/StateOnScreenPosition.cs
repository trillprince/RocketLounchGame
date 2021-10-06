using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class StateOnScreenPosition
{
    private readonly Transform _transform;
    private readonly Vector3 _screenBounds;
    private StateOnScreen _currentStateOnScreen;
    protected readonly ISpaceObjectController SpaceObjectController;

    protected StateOnScreenPosition(
        Transform transform,
        ISpaceObjectController spaceObjectController)
    {
        _transform = transform;
        SpaceObjectController = spaceObjectController;
        
            _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - _transform.position.z));
    }

    protected virtual void OnStateChange(StateOnScreen state)
    {
        switch (state)
        {
            case StateOnScreen.UpperRed:
                break;
            case StateOnScreen.Yellow:
                break;
            case StateOnScreen.Green:
                break;
            case StateOnScreen.LowerRed:
                break;
            case StateOnScreen.DisposeZone:
                SpaceObjectController.DisposeLastObject();
                break;
        }
    }
    
    public void StateCheck()
    {
        if (_transform.position.y < -_screenBounds.y && 
            _transform.position.y >= -_screenBounds.y * 0.5f &&
            _currentStateOnScreen != StateOnScreen.LowerRed)
        {
            _currentStateOnScreen = StateOnScreen.UpperRed;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < -_screenBounds.y * 0.5f &&
                 _transform.position.y >= 0 &&
                 _currentStateOnScreen != StateOnScreen.Yellow)
        {
            _currentStateOnScreen = StateOnScreen.Yellow;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < 0 &&
                 _transform.position.y >= _screenBounds.y * 0.5f &&
                 _currentStateOnScreen != StateOnScreen.Green)
        {
            _currentStateOnScreen = StateOnScreen.Green;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < _screenBounds.y * 0.5f &&
                 _transform.position.y >= _screenBounds.y &&
                 _currentStateOnScreen != StateOnScreen.LowerRed)
        {
            _currentStateOnScreen = StateOnScreen.LowerRed;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < _screenBounds.y && _currentStateOnScreen != StateOnScreen.DisposeZone )
        {
            _currentStateOnScreen = StateOnScreen.DisposeZone;
            OnStateChange(_currentStateOnScreen);
        }
        
        
    }
}