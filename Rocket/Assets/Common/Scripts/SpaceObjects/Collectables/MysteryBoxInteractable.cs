using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class MysteryBoxInteractable: IInteractable
{
    
    private readonly RocketInventory _rocketInventory;
    private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
    private ISpaceObject _spaceObject;
    private readonly IInventory _inventory;

    public MysteryBoxInteractable(RocketInventory rocketInventory,ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject,IInventory inventory)
    {
        _rocketInventory = rocketInventory;
        _spaceObjectLifeCycle = spaceObjectLifeCycle;
        _spaceObject = spaceObject;
        _inventory = inventory;
    }

    public void Interact()
    {
        _rocketInventory.AddCollectable(_inventory.GetCollectable());
        _spaceObjectLifeCycle.Dispose(_spaceObject);
    }
}