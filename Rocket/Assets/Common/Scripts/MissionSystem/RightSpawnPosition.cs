using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public RightSpawnPosition(RocketMovementController rocketMovementController, SphereCollider asteroidCollider): base(rocketMovementController,asteroidCollider)
        {
           
        }   

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                -_screenBounds.x - _asteroidCollider.radius * 2,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}