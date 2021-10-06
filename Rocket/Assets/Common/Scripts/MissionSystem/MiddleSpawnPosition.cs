using System;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public MiddleSpawnPosition(RocketMovementController rocketMovementController, SphereCollider asteroidCollider): base(rocketMovementController,asteroidCollider)
        {

        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (_rocketPosition.x),
                -_screenBounds.y ,
                _rocketPosition.z);
        }
    }
}