using Common.Scripts.Rocket;

public class HologramInteractable : IInteractable
{
    private readonly CollectableDisposer _disposer;
    private readonly RocketBoosterController _rocketBoosterController;
    private readonly RocketHealth _rocketHealth;
    private readonly RocketGraphics _rocketGraphics;
    private readonly RocketEffect _rocketEffect;

    public HologramInteractable(CollectableDisposer disposer, RocketBoosterController rocketBoosterController,
        RocketHealth rocketHealth,RocketGraphics rocketGraphics)
    {
        _disposer = disposer;
        _rocketBoosterController = rocketBoosterController;
        _rocketHealth = rocketHealth;
        _rocketGraphics = rocketGraphics;
        _rocketEffect = new HologramEffect(rocketHealth,new HologramAudio());
    }

    public void Interact()
    {
        
        _rocketBoosterController.ApplyHealthBooster(_rocketEffect);
        _disposer.DisposeCollectable();
    }
}