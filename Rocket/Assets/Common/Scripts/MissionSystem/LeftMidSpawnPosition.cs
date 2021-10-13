using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftMidSpawnPosition : SpawnPosition, ISpawnPosition
    {
        private readonly LeftSpawnPosition _leftSpawnPosition;
        private readonly MiddleSpawnPosition _middleSpawnPosition;

        public LeftMidSpawnPosition(RocketMovement rocketMovement, 
            LeftSpawnPosition leftSpawnPosition, 
            MiddleSpawnPosition middleSpawnPosition) : base(rocketMovement)
        {
            _leftSpawnPosition = leftSpawnPosition;
            _middleSpawnPosition = middleSpawnPosition;
        }

        public Vector3 GetSpawnPosition(Collider collider)
        {
            return new Vector3(
                (_leftSpawnPosition.GetSpawnPosition(collider).x - _middleSpawnPosition.GetSpawnPosition(collider).x) / 2,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}