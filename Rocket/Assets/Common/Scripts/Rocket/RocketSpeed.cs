using System;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketSpeed : MonoBehaviour, ISpeed

    {
        private static float _currentSpeed = 200;
        private int _speedStep = 20;
        private static Vector3 _rocketDirection;
        private int _maxSpeed = 320;


        [Inject]
        private void Constructor(ILevelInfo levelInfo)
        {
            levelInfo.OnNextLevel += OnNextLevel;
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