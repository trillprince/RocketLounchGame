using Common.Scripts.Rocket;

namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinInteractable: IInteractable
    {
        private readonly CollectableDisposer _disposer;
        private readonly RocketInventory _rocketInventory;
        private readonly RocketAudio _rocketAudio;

        public DogCoinInteractable(CollectableDisposer disposer,RocketInventory rocketInventory,RocketAudio rocketAudio)
        {
            _disposer = disposer;
            _rocketInventory = rocketInventory;
            _rocketAudio = rocketAudio;
        }

        public void Interact()
        {
            _rocketInventory.AddCoinValue(1);
            _disposer.DisposeCollectable();
            _rocketAudio.GetAudioManager().FxAudioClipSetActive("Coin Collect",true);
        }

        public void Execute()
        {
            
        }
    }
}