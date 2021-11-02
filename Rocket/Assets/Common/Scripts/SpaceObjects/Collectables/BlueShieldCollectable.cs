using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Audio;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

public class BlueShieldCollectable : SpaceObject
{
    private IInteractable _interactable;
    private ICollectableDisposer _disposer;
    private IMoveComponent _movable;
    private MysteryBoxStateOnScreen _collectableStateOnScreen;
    public GameObject _effectGameObject;
    private BlueShieldAudio _collectableAudio;

    public override void Constructor(RocketController rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
        GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition,
        IAudioManager audioManager)
    {
        base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController,
            spawnPosition, audioManager);

        _collectableStateOnScreen = new MysteryBoxStateOnScreen(transform, spaceObjectLifeCycle, this);
        _disposer = new CollectableDisposer(spaceObjectLifeCycle, this);
        _collectableAudio = new BlueShieldAudio(audioManager);
        _interactable = new BlueShieldInteractable(_disposer, rocketController.BoosterController,
            rocketController.Health, _effectGameObject,_collectableAudio);
        _movable = new CollectableMove(rocketController.Movement, transform);
    }

    public override void Interact()
    {
        _interactable.Interact();
    }

    public override void Execute()
    {
        _movable.Move();
        _collectableStateOnScreen.StateCheck();
    }
}