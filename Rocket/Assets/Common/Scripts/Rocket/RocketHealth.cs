using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        private int _health  = 2;

        public RocketHealth(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
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
        
        

    }
}