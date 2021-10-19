using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class MysteryBoxStateOnScreen: StateOnScreenPosition
{
    public MysteryBoxStateOnScreen(Transform transform, ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject) : base(transform, spaceObjectLifeCycle,spaceObject)
    {
        
    }
}