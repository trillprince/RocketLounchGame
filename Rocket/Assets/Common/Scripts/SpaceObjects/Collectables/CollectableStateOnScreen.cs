using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class CollectableStateOnScreen: StateOnScreenPosition
{
    public CollectableStateOnScreen(Transform transform, ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject) : base(transform, spaceObjectLifeCycle,spaceObject)
    {
        
    }
}