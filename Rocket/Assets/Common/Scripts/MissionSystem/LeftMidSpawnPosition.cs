using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftMidSpawnPosition : SpawnPosition, ISpawnPosition
    {
        private readonly LeftSpawnPosition _leftSpawnPosition;
        private readonly MiddleSpawnPosition _middleSpawnPosition;

        public LeftMidSpawnPosition(RocketMovementController rocketMovementController, 
            LeftSpawnPosition leftSpawnPosition, 
            MiddleSpawnPosition middleSpawnPosition,SphereCollider asteroidCollider) : base(rocketMovementController,asteroidCollider)
        {
            _leftSpawnPosition = leftSpawnPosition;
            _middleSpawnPosition = middleSpawnPosition;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (_leftSpawnPosition.GetSpawnPosition().x - _middleSpawnPosition.GetSpawnPosition().x) / 2,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}