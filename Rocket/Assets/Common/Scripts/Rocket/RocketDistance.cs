using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketDistance : IGameStateSubscriber,IUpdatable
    {
        private readonly RocketSpeed _rocketSpeed;
        public float CoveredDistance { get; private set; }
        private bool _launched;

        public RocketDistance(RocketSpeed rocketSpeed)
        {
            _rocketSpeed = rocketSpeed;
        }

        public void OnGameStateChange(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                _launched = true;
            }
        }

        private void CalculateCoveredDistance()
        {
            if(!_launched) return;
            CoveredDistance += _rocketSpeed.GetCurrentSpeed() * Time.deltaTime;
        }

        public void Execute()
        {
            CalculateCoveredDistance();
        }
    }
}