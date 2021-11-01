using Common.Scripts.Boosters;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using UnityEngine;

public class BlueShieldInteractable : IInteractable
{
    private readonly ICollectableDisposer _collectableDisposer;
    private readonly RocketBoosterController _rocketBoosterController;
    private readonly RocketEffect _rocketEffect;

    public BlueShieldInteractable(ICollectableDisposer collectableDisposer,
        RocketBoosterController rocketBoosterController, RocketHealth rocketHealth, GameObject effectGameObject)
    {
        _collectableDisposer = collectableDisposer;
        _rocketBoosterController = rocketBoosterController;
        _rocketEffect = new BlueShieldEffect(rocketHealth,effectGameObject);
    }

    public void Interact()
    {
        _rocketBoosterController.ApplyHealthBooster(_rocketEffect);
        _collectableDisposer.DisposeCollectable();
    }
}