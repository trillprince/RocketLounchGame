using System.Collections;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketDistance : IGameStateSubscriber,IUpdatable
    {
        private readonly RocketSpeed _rocketSpeed;
        private readonly PlayerDataSaver _playerDataSaver;
        public float CoveredDistance { get; private set; }
        private bool _launched;

        public RocketDistance(RocketSpeed rocketSpeed, PlayerDataSaver playerDataSaver)
        {
            _rocketSpeed = rocketSpeed;
            _playerDataSaver = playerDataSaver;
        }

        public void OnGameStateChange(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                _launched = true;
            }
            else if (gameState == GameState.EndOfGame)
            {
                _playerDataSaver.SaveScore((int)CoveredDistance);
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