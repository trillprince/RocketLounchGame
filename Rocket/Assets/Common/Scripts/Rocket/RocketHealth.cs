using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        private int _health;
        private int _startHealth = 2;

        public RocketHealth(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _health = _startHealth;
        }

        public void DamageRocket(int damageValue)
        {
            if (_health > damageValue)
            {
                _health -= damageValue;
                return;
            }
            _health = 0;
            _gameStateController.SetGameState(GameState.EndOfGame);
            
        }

        public void RestoreHealth()
        {
            _health = _startHealth;
        }
        
        

    }
}