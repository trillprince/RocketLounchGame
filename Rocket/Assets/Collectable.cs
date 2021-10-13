using System.Collections;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;

public class Collectable : SpaceObject
{
    private IInteractable _interactable;
    private IMoveComponent _movable;

    public override void Constructor(RocketController rocketController, ISpaceObjectController spaceObjectController,
        GameLoopController gameLoopController, GameStateController gameStateController, ISpawnPosition spawnPosition)
    {
        base.Constructor(rocketController, spaceObjectController, gameLoopController, gameStateController, spawnPosition);

        _interactable = new CollectableInteractable();
        _movable = new CollectableMove(rocketController.Movement,transform);
    }

    public override void Interact()
    {
        _interactable.Interact();
    }

    public override void Execute()
    {
        _movable.Move();
    }
}