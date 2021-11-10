namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinInteractable: IInteractable
    {
        private readonly CollectableDisposer _disposer;

        public DogCoinInteractable(CollectableDisposer disposer)
        {
            _disposer = disposer;
        }

        public void Interact()
        {
            _disposer.DisposeCollectable();
        }
    }
}