using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;
using Zenject;

namespace Common.Scripts.SpaceObjects.Asteroid
{
    public class Asteroid : SpaceObject
    {
        private AsteroidStateOnScreen _asteroidStateOnScreen;
        private AsteroidMove _asteroidMove;
        private AsteroidDelivery _asteroidDelivery;
        private ISpawnPosition _spawnPosition;
        private AsteroidInteraction _asteroidInteraction;
        
        
        public override void Constructor(RocketController rocketController,
            ISpaceObjectLifeCycle spaceObjectLifeCycle,
            GameLoopController gameLoopController, IGameStateController gameStateController,
            ISpawnPosition spawnPosition)
        {
            base.Constructor(rocketController, spaceObjectLifeCycle, gameLoopController, gameStateController, spawnPosition);
            
            _asteroidMove = new AsteroidMove(rocketController.Movement, transform);
            _spawnPosition = spawnPosition;
            _asteroidDelivery = new AsteroidDelivery(spaceObjectLifeCycle,gameLoopController);
            _asteroidStateOnScreen = new AsteroidStateOnScreen( 
                transform,
                spaceObjectLifeCycle,
                _asteroidDelivery,
                this
            );
            _asteroidInteraction = new AsteroidInteraction(rocketController.Health,
                _asteroidStateOnScreen.SpaceObjectLifeCycle,
                this);
        }
        
        public override void Execute()
        {
            _asteroidMove.Move();
            _asteroidStateOnScreen.StateCheck();
        }

        public override void Interact()
        {
            _asteroidInteraction.Interact();
        }
    }
}
