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
            MiddleSpawnPosition middleSpawnPosition,MeshCollider meshCollider) : base(rocketMovementController,meshCollider)
        {
            _leftSpawnPosition = leftSpawnPosition;
            _middleSpawnPosition = middleSpawnPosition;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (_leftSpawnPosition.GetSpawnPosition().x - _middleSpawnPosition.GetSpawnPosition().x) / 2,
                -_screenBounds.y + _meshCollider.bounds.size.y / 2,
                _rocketPosition.z);
        }
    }
}