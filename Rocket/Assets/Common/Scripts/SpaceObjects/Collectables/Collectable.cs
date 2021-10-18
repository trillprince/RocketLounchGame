using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;

public class Collectable : SpaceObject
{
    private IInteractable _interactable;
    private IMoveComponent _movable;
    private ICollectable _collectable;
    private CollectableStateOnScreen _collectableStateOnScreen;

    public override void Constructor(RocketController rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
        GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition)
    {
        base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController, spawnPosition);
        _collectable = new CollectableBox();
        _collectableStateOnScreen = new CollectableStateOnScreen(transform, spaceObjectLifeCycle,this);
        _interactable = new CollectableInteractable(rocketController.Inventory,spaceObjectLifeCycle,this);
        _movable = new CollectableMove(rocketController.Movement,transform);
    }

    public override void Interact()
    {
        base.Interact();
        _interactable.Interact();
    }

    public override void Execute()
    {
        _movable.Move();
        _collectableStateOnScreen.StateCheck();
    }

}