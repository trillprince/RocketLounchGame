using System;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        private int _currentRepairs;
        private int _maxRepairs = 2;
        public event Action OnDamage;

        public RocketHealth(IGameStateController gameStateController,ILevelInfo levelInfo)
        {
            _gameStateController = gameStateController;
            levelInfo.OnNextLevel += RestoreHealth;
            _currentRepairs = _maxRepairs;
        }

        public void DamageRocket(int damageValue)
        {
            OnDamage?.Invoke();
            if (_currentRepairs >= damageValue)
            {
                _currentRepairs -= damageValue;
                return;
            }
            _gameStateController.SetGameState(GameState.EndOfGame);
            
        }

        private void RestoreHealth()
        {
            if (_currentRepairs + 1 > _maxRepairs)
            {
                return;
            }
            _currentRepairs += 1;
        }
        
        

    }
}