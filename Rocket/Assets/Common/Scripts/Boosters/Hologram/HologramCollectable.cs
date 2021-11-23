using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Audio;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public class HologramCollectable : SpaceObject
{
    private CollectableDisposer _disposer;
    private StateOnScreen _stateOnScreen;
    private CollectableMove _movable;
    private IInteractable _interactable;

    public override void Constructor(RocketController rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
        GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition,
        IAudioManager audioManager)
    {
        base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController,
            spawnPosition, audioManager);

        _stateOnScreen = new StateOnScreen(transform, spaceObjectLifeCycle, this);
        _disposer = new CollectableDisposer(spaceObjectLifeCycle, this);
        _interactable = new HologramInteractable(_disposer, rocketController.BoosterController,
            rocketController,rocketController.Graphics,audioManager);
        _movable = new CollectableMove(rocketController.Movement, transform);
    }

    public override void Interact()
    {
        _interactable.Interact();
    }

    public override void Execute()
    {
        _movable.Move();
        _stateOnScreen.StateCheck();
    }
}