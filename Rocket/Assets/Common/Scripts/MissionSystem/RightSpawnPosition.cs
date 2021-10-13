using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public RightSpawnPosition(RocketMovement rocketMovement, Collider asteroidCollider): base(rocketMovement,asteroidCollider)
        {
           
        }   

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                -_screenBounds.x - _asteroidCollider.transform.localScale.x,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}