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
            ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController, GameStateController gameStateController,
            ISpawnPosition spawnPosition)
        {
            base.Constructor(rocketController, spaceObjectController, gameLoopController, gameStateController, spawnPosition);
            
            _asteroidMove = new AsteroidMove(rocketController.Movement, transform);
            _spawnPosition = spawnPosition;
            _asteroidDelivery = new AsteroidDelivery(spaceObjectController,gameLoopController);
            _asteroidInteraction = new AsteroidInteraction(gameStateController,rocketController.Health);
            _asteroidStateOnScreen = new AsteroidStateOnScreen( 
                transform,
                spaceObjectController,
                _asteroidDelivery
            );
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
