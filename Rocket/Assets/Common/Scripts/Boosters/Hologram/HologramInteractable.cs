using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;

public class HologramInteractable : IInteractable
{
    private readonly CollectableDisposer _disposer;
    private readonly RocketBoosterController _rocketBoosterController;
    private readonly RocketController _rocketController;
    private readonly RocketGraphics _rocketGraphics;
    private readonly RocketEffect _rocketEffect;

    public HologramInteractable(CollectableDisposer disposer, RocketBoosterController rocketBoosterController,
        RocketController rocketController, RocketGraphics rocketGraphics)
    {
        _disposer = disposer;
        _rocketBoosterController = rocketBoosterController;
        _rocketController = rocketController;
        _rocketGraphics = rocketGraphics;
    }

    public void Interact()
    {
        _rocketGraphics.SetShader(Shader.Find("Shader Graphs/Hologram"));
        _rocketBoosterController.ApplyBooster(new HologramEffect(_rocketController, new HologramAudio()));
        _disposer.DisposeCollectable();
    }

}