using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    internal class MiddleSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public MiddleSpawnPosition(RocketMovementController rocketMovementController): base(rocketMovementController)
        {

        }

        public Vector3 GetSpawnPosition(MeshCollider meshCollider)
        {
            return new Vector3(
                (_rocketPosition.x) / 2,
                -_screenBounds.y + meshCollider.bounds.size.y / 2,
                _rocketPosition.z);
        }
    }
}