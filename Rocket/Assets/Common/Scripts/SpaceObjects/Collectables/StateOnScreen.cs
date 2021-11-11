using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class StateOnScreen: StateOnScreenPosition
{
    public StateOnScreen(Transform transform, ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject) : base(transform, spaceObjectLifeCycle,spaceObject)
    {
        
    }
}