using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;

public class MysteryBox : SpaceObject
{
    private IInteractable _interactable;
    private IMoveComponent _movable;
    private IInventory _inventory;
    private MysteryBoxStateOnScreen _mysteryBoxStateOnScreen;

    public override void Constructor(RocketController rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
        GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition)
    {
        base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController, spawnPosition);
        _inventory = new MysteryBoxInventory();
        _mysteryBoxStateOnScreen = new MysteryBoxStateOnScreen(transform, spaceObjectLifeCycle,this);
        _interactable = new MysteryBoxInteractable(rocketController.Inventory,spaceObjectLifeCycle,this,_inventory);
        _movable = new MysteryBoxMove(rocketController.Movement,transform);
    }

    public override void Interact()
    {
        _interactable.Interact();
    }

    public override void Execute()
    {
        _movable.Move();
        _mysteryBoxStateOnScreen.StateCheck();
    }

}