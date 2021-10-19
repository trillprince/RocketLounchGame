using System;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketSpeed : MonoBehaviour, ISpeed

    {
        private static float _currentSpeed = 150;
        private int _speedStep = 25;
        private static Vector3 _rocketDirection;
        private IGameLoopController _gameLoopController;
        private int _maxSpeed = 250;


        [Inject]
        private void Constructor(IGameLoopController gameLoopController)
        {
            _gameLoopController = gameLoopController;
        }

        private void Start()
        {
            _gameLoopController.GetLevelInfo().OnNextLevel += OnNextLevel;
        }

        private void OnNextLevel()
        {
            AddSpeed(_speedStep);
        }

        public static float CurrentSpeed
        {
            get => _currentSpeed;
        }

        public static Vector3 RocketDirection
        {
            get => _rocketDirection;
            private set => _rocketDirection = value;
        }
        void Update()
        {
            CheckRocketDirection();
        }

        private void CheckRocketDirection()
        {
            RocketDirection = transform.up;
        }

        public static Vector3 GetRocketDirection()
        {
            return RocketDirection;
        }

        public void AddSpeed(int value)
        {
            if (_currentSpeed < _maxSpeed)
            {
                var newSpeed = _currentSpeed + value;
                _currentSpeed = Mathf.Lerp(_currentSpeed, newSpeed, 2);
            }
        }

    }
}