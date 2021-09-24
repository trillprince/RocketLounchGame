using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using UnityEngine;

public class StateOnScreenPosition
{
    protected readonly Transform Transform;
    protected readonly Vector3 ScreenBounds;
    protected StateOnScreen CurrentStateOnScreen;
    protected readonly MeshCollider MeshCollider;
    protected readonly ISpaceObjectController SpaceObjectController;
    protected readonly GameLoopController GameLoopController;
    protected readonly Dictionary<StateOnScreen, Action> _actionsOnStates;
    private bool _hasDictOfActions;

    protected StateOnScreenPosition(MeshCollider meshCollider,
        Transform transform,
        ISpaceObjectController spaceObjectController,
        GameLoopController gameLoopController,
        Dictionary<StateOnScreen, Action> actionsOnStates = null)
    {
        MeshCollider = meshCollider;
        Transform = transform;
        SpaceObjectController = spaceObjectController;
        GameLoopController = gameLoopController;
        _actionsOnStates = actionsOnStates;
        if (actionsOnStates != null)
        {
            _hasDictOfActions = true;
        }
        
        if (UnityEngine.Camera.main is { })
            ScreenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - Transform.position.z));
    }

    public virtual void StateCheck()
    {
        if (Transform.position.y < -ScreenBounds.y && 
            Transform.position.y >= -ScreenBounds.y * 0.5f &&
            CurrentStateOnScreen != StateOnScreen.UpperRed
        )
        {
            CurrentStateOnScreen = StateOnScreen.UpperRed;
            if (_hasDictOfActions && _actionsOnStates.ContainsKey(CurrentStateOnScreen))
            {
                _actionsOnStates[CurrentStateOnScreen]?.Invoke();
            }
        }
        else if (Transform.position.y < -ScreenBounds.y * 0.5f &&
                 Transform.position.y >= 0 &&
                 CurrentStateOnScreen != StateOnScreen.Yellow
        )
        {
            CurrentStateOnScreen = StateOnScreen.Yellow;
            if (_hasDictOfActions && _actionsOnStates.ContainsKey(CurrentStateOnScreen))
            {
                _actionsOnStates[CurrentStateOnScreen]?.Invoke();
            }
        }
        else if (Transform.position.y < 0 &&
                 Transform.position.y >= ScreenBounds.y * 0.5f &&
                 CurrentStateOnScreen != StateOnScreen.Green
        )
        {
            CurrentStateOnScreen = StateOnScreen.Green;
            if (_hasDictOfActions && _actionsOnStates.ContainsKey(CurrentStateOnScreen))
            {
                _actionsOnStates[CurrentStateOnScreen]?.Invoke();
            }
        }
        else if (Transform.position.y < ScreenBounds.y * 0.5f &&
                 Transform.position.y >= ScreenBounds.y &&
                 CurrentStateOnScreen != StateOnScreen.LowerRed
        )
        {
            CurrentStateOnScreen = StateOnScreen.LowerRed;
            if (_hasDictOfActions && _actionsOnStates.ContainsKey(CurrentStateOnScreen))
            {
                _actionsOnStates[CurrentStateOnScreen]?.Invoke();
            }
            SpaceObjectController.ScopeToNextObject();
        }
        else if (Transform.position.y < ScreenBounds.y - MeshCollider.bounds.size.y &&
                 CurrentStateOnScreen != StateOnScreen.Dispose)
        {
            if (_hasDictOfActions && _actionsOnStates.ContainsKey(CurrentStateOnScreen))
            {
                _actionsOnStates[CurrentStateOnScreen]?.Invoke();
            }
            SpaceObjectController.DisposeLastObject();
        }
        
        
    }
}