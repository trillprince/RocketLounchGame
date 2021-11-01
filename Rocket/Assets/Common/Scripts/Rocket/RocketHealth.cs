using System;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        public int CurrentHealth { get; private set; }

        public int _maxHealth = 3;
        public event Action OnDamage;

        public RocketHealth(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            CurrentHealth = 1;
        }

        public void DamageRocket(int value)
        {
            if (CurrentHealth - value > 0)
            {
                CurrentHealth -= value;
                Debug.Log(CurrentHealth);
                OnDamage?.Invoke();
                return;
            }
            OnDamage?.Invoke();
            _gameStateController.SetGameState(GameState.EndOfGame);
            
        }

        public void AddHealth(int value)
        {
            if (CurrentHealth + value ! > _maxHealth)
            {
                CurrentHealth += value;
            }
            else
            {
                CurrentHealth = _maxHealth;
            }
        }

    }
}