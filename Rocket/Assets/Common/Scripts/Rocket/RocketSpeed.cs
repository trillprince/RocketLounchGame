using System;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketSpeed : MonoBehaviour, ISpeed

    {
        private int _currentSpeed = 200;
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

        public  int CurrentSpeed
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

        public  Vector3 GetRocketDirection()
        {
            return RocketDirection;
        }

        public void AddSpeed(int value)
        {
            if (_currentSpeed < _maxSpeed)
            {
                _currentSpeed += value;
            }
        }


        public int GetCurrentSpeed()
        {
            return _currentSpeed;
        }
    }
}