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

    protected StateOnScreenPosition(MeshCollider meshCollider,
        Transform transform,
        ISpaceObjectController spaceObjectController,
        GameLoopController gameLoopController)
    {
        MeshCollider = meshCollider;
        Transform = transform;
        SpaceObjectController = spaceObjectController;
        GameLoopController = gameLoopController;
        
            ScreenBounds =
                UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height,
                    UnityEngine.Camera.main.transform.position.z - Transform.position.z));
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
                SpaceObjectController.ScopeToNextObject();
                break;
            case StateOnScreen.DisposeZone:
                SpaceObjectController.DisposeLastObject();
                break;
        }
    }
    
    public void StateCheck()
    {
        if (Transform.position.y < -ScreenBounds.y && 
            Transform.position.y >= -ScreenBounds.y * 0.5f &&
            CurrentStateOnScreen != StateOnScreen.LowerRed)
        {
            CurrentStateOnScreen = StateOnScreen.UpperRed;
            OnStateChange(CurrentStateOnScreen);
        }
        else if (Transform.position.y < -ScreenBounds.y * 0.5f &&
                 Transform.position.y >= 0 &&
                 CurrentStateOnScreen != StateOnScreen.Yellow)
        {
            CurrentStateOnScreen = StateOnScreen.Yellow;
            OnStateChange(CurrentStateOnScreen);
        }
        else if (Transform.position.y < 0 &&
                 Transform.position.y >= ScreenBounds.y * 0.5f &&
                 CurrentStateOnScreen != StateOnScreen.Green)
        {
            CurrentStateOnScreen = StateOnScreen.Green;
            OnStateChange(CurrentStateOnScreen);
        }
        else if (Transform.position.y < ScreenBounds.y * 0.5f &&
                 Transform.position.y >= ScreenBounds.y &&
                 CurrentStateOnScreen != StateOnScreen.LowerRed)
        {
            CurrentStateOnScreen = StateOnScreen.LowerRed;
            OnStateChange(CurrentStateOnScreen);
        }
        else if (Transform.position.y < ScreenBounds.y - MeshCollider.bounds.size.y && CurrentStateOnScreen != StateOnScreen.DisposeZone )
        {
            CurrentStateOnScreen = StateOnScreen.DisposeZone;
            OnStateChange(CurrentStateOnScreen);
        }
        
        
    }
}