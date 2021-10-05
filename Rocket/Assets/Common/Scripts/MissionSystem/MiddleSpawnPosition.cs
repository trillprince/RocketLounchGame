using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public MiddleSpawnPosition(RocketMovementController rocketMovementController,MeshCollider meshCollider): base(rocketMovementController,meshCollider)
        {

        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (_rocketPosition.x) / 2,
                -_screenBounds.y + _meshCollider.bounds.size.y / 2,
                _rocketPosition.z);
        }
    }
}