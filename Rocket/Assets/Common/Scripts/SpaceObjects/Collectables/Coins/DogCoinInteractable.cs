using Common.Scripts.Rocket;

namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinInteractable: IInteractable
    {
        private readonly CollectableDisposer _disposer;
        private readonly RocketInventory _rocketInventory;

        public DogCoinInteractable(CollectableDisposer disposer,RocketInventory rocketInventory)
        {
            _disposer = disposer;
            _rocketInventory = rocketInventory;
        }

        public void Interact()
        {
            _rocketInventory.AddCoinValue(1);
            _disposer.DisposeCollectable();
        }
    }
}