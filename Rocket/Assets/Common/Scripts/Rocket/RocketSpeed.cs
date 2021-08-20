using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketSpeed : MonoBehaviour
    {
        [SerializeField] private RocketSpeedStats _rocketSpeedStats;
        private static float _currentSpeed;
        private static Vector3 _rocketDirection;

        public static float CurrentSpeed
        {
            get => _currentSpeed;
        }

        public static Vector3 RocketDirection
        {
            get => _rocketDirection;
            private set => _rocketDirection = value;
        }

        private void Start()
        {
            _currentSpeed = _rocketSpeedStats.RocketStartSpeed;
        }

        void Update()
        {
            CalculateSpeed();
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

        private void CalculateSpeed()
        {
            SpeedCalculator.CalculateSpeed(
                ref _currentSpeed,
                _rocketSpeedStats.RocketMaxSpeed,
                _rocketSpeedStats.RocketSpeedAcceleration);
        }
    }
}