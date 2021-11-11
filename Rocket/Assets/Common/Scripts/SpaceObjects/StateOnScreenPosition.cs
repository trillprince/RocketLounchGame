using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class StateOnScreenPosition
{
    private readonly Transform _transform;
    private readonly Vector3 _screenBounds;
    private Common.Scripts.SpaceObjects.StateOnScreen _currentStateOnScreen;
    public readonly ISpaceObjectLifeCycle SpaceObjectLifeCycle;
    private ISpaceObject _spaceObject;

    protected StateOnScreenPosition(
        Transform transform,
        ISpaceObjectLifeCycle spaceObjectLifeCycle,
        ISpaceObject spaceObject)
    {
        _transform = transform;
        SpaceObjectLifeCycle = spaceObjectLifeCycle;
        _spaceObject = spaceObject;

        _screenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - _transform.position.z));
    }

    protected virtual void OnStateChange(Common.Scripts.SpaceObjects.StateOnScreen state)
    {
        switch (state)
        {
            case Common.Scripts.SpaceObjects.StateOnScreen.UpperRed:
                break;
            case Common.Scripts.SpaceObjects.StateOnScreen.Yellow:
                break;
            case Common.Scripts.SpaceObjects.StateOnScreen.Green:
                break;
            case Common.Scripts.SpaceObjects.StateOnScreen.LowerRed:
                break;
            case Common.Scripts.SpaceObjects.StateOnScreen.DisposeZone:
                SpaceObjectLifeCycle.Dispose(_spaceObject);
                break;
        }
    }
    
    public void StateCheck()
    {
        if (_transform.position.y < -_screenBounds.y && 
            _transform.position.y >= -_screenBounds.y * 0.5f &&
            _currentStateOnScreen != Common.Scripts.SpaceObjects.StateOnScreen.LowerRed)
        {
            _currentStateOnScreen = Common.Scripts.SpaceObjects.StateOnScreen.UpperRed;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < -_screenBounds.y * 0.5f &&
                 _transform.position.y >= 0 &&
                 _currentStateOnScreen != Common.Scripts.SpaceObjects.StateOnScreen.Yellow)
        {
            _currentStateOnScreen = Common.Scripts.SpaceObjects.StateOnScreen.Yellow;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < 0 &&
                 _transform.position.y >= _screenBounds.y * 0.5f &&
                 _currentStateOnScreen != Common.Scripts.SpaceObjects.StateOnScreen.Green)
        {
            _currentStateOnScreen = Common.Scripts.SpaceObjects.StateOnScreen.Green;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < _screenBounds.y * 0.5f &&
                 _transform.position.y >= _screenBounds.y &&
                 _currentStateOnScreen != Common.Scripts.SpaceObjects.StateOnScreen.LowerRed)
        {
            _currentStateOnScreen = Common.Scripts.SpaceObjects.StateOnScreen.LowerRed;
            OnStateChange(_currentStateOnScreen);
        }
        else if (_transform.position.y < _screenBounds.y && _currentStateOnScreen != Common.Scripts.SpaceObjects.StateOnScreen.DisposeZone )
        {
            _currentStateOnScreen = Common.Scripts.SpaceObjects.StateOnScreen.DisposeZone;
            OnStateChange(_currentStateOnScreen);
        }
        
        
    }
}