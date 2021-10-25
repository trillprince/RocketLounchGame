using System;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        public int CurrentRepairs { get; private set; }
        private int _maxRepairs = 2;
        public event Action OnDamage;

        public RocketHealth(IGameStateController gameStateController,ILevelInfo levelInfo)
        {
            _gameStateController = gameStateController;
            levelInfo.OnNextLevel += RestoreHealth;
            CurrentRepairs = _maxRepairs;
        }

        public void DamageRocket()
        {
            
            if (CurrentRepairs > 0)
            {
                CurrentRepairs -= 1;
                OnDamage?.Invoke();
                return;
            }
            OnDamage?.Invoke();
            _gameStateController.SetGameState(GameState.EndOfGame);
            
        }

        private void RestoreHealth()
        {
            if (CurrentRepairs + 1 > _maxRepairs)
            {
                return;
            }
            CurrentRepairs += 1;
        }
        
        

    }
}