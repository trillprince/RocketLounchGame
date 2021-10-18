using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class CollectableInteractable: IInteractable
{
    
    private readonly RocketInventory _rocketInventory;
    private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
    private ISpaceObject _spaceObject;

    public CollectableInteractable(RocketInventory rocketInventory,ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject)
    {
        _rocketInventory = rocketInventory;
        _spaceObjectLifeCycle = spaceObjectLifeCycle;
        _spaceObject = spaceObject;
    }

    public void Interact()
    {
        _spaceObjectLifeCycle.Dispose(_spaceObject);
    }
}