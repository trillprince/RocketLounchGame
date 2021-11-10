using System.Data;
using Common.Scripts.Audio;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects.Asteroid;

namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinCollectable : SpaceObject
    {
        private CollectableMove _movable;
        private CollectableDisposer _disposer;
        private DogCoinStateOnScreen _collectableStateOnScreen;
        private DogCoinInteractable _interactable;

        public override void Constructor(RocketController
                rocketController, ISpaceObjectLifeCycle spaceObjectLifeCycle,
            GameLoopController gameLoopController, IGameStateController gameStateController, ISpawnPosition spawnPosition,
            IAudioManager audioManager)
        {
            base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController,
                spawnPosition, audioManager);

            _collectableStateOnScreen = new DogCoinStateOnScreen(transform, spaceObjectLifeCycle, this);
            _disposer = new CollectableDisposer(spaceObjectLifeCycle, this);
            _interactable = new DogCoinInteractable(_disposer);
            _movable = new DogCoinMove(rocketController.Movement, transform);
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
}