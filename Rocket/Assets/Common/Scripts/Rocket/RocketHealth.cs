using System;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketHealth
    {
        private readonly IGameStateController _gameStateController;
        public int CurrentHealth { get; private set; }

        public event Action OnDamage;
        public event Action OnRocketDestroy;

        public RocketHealth(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            CurrentHealth = 0;
        }

        public void DamageRocket(int value)
        {
            if (CurrentHealth - value >= 0)
            {
                CurrentHealth -= value;
                OnDamage?.Invoke();
            }
            else if (CurrentHealth - value < 0)
            {
                OnRocketDestroy?.Invoke();
                _gameStateController.SetGameState(GameState.EndOfGame);
            }
        }

        public void AddHealth(int value)
        {
            CurrentHealth += value;
        }
    }
}