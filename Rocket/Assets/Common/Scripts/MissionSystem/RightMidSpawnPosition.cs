using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightMidSpawnPosition : SpawnPosition, ISpawnPosition
    {
        private readonly RightSpawnPosition _rightSpawnPosition;
        private readonly MiddleSpawnPosition _middleSpawnPosition;

        public RightMidSpawnPosition(RocketMovement rocketMovement,
            RightSpawnPosition rightSpawnPosition,
            MiddleSpawnPosition middleSpawnPosition): base(rocketMovement)
        {
            _rightSpawnPosition = rightSpawnPosition;
            _middleSpawnPosition = middleSpawnPosition;
        }

        public Vector3 GetSpawnPosition(Collider collider)
        {
            return new Vector3(
                (_rightSpawnPosition.GetSpawnPosition(collider).x + _middleSpawnPosition.GetSpawnPosition(collider).x) / 2,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}