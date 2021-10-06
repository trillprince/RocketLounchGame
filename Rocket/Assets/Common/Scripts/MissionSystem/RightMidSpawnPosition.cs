using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightMidSpawnPosition : SpawnPosition, ISpawnPosition
    {
        private readonly RightSpawnPosition _rightSpawnPosition;
        private readonly MiddleSpawnPosition _middleSpawnPosition;

        public RightMidSpawnPosition(RocketMovementController rocketMovementController,
            RightSpawnPosition rightSpawnPosition,
            MiddleSpawnPosition middleSpawnPosition, SphereCollider collider): base(rocketMovementController,collider)
        {
            _rightSpawnPosition = rightSpawnPosition;
            _middleSpawnPosition = middleSpawnPosition;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (_rightSpawnPosition.GetSpawnPosition().x + _middleSpawnPosition.GetSpawnPosition().x) / 2,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}