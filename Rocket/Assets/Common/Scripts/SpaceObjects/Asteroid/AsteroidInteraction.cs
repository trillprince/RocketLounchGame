using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class AsteroidInteraction: IInteractable
    {
        private GameStateController _gameStateController;
        private RocketHealth _rocketHealth;

        public AsteroidInteraction(GameStateController gameStateController,RocketHealth rocketHealth)
        {
            _gameStateController = gameStateController;
            _rocketHealth = rocketHealth;
        }

        public void Interact()
        {
            if (_rocketHealth.Health > 1)
            {
                _rocketHealth.Health -= 1;
                return;
            }
            _rocketHealth.Health = 0;
            _gameStateController.SetGameState(GameState.EndOfGame);
        }
    }
}