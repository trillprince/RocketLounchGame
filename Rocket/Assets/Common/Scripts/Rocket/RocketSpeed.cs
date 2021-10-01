using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketSpeed : MonoBehaviour
    {
        private static float _currentSpeed;
        private static Vector3 _rocketDirection;
        private MilesCount _milesCount;

        [Inject]
        public void Constructor(MilesCount milesCount)
        {
            _milesCount = milesCount;
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

        private void Start()
        {
            _currentSpeed = 150;
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

    }
}