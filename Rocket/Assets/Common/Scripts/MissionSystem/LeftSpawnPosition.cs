using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public LeftSpawnPosition(RocketMovementController rocketMovementController, SphereCollider asteroidCollider): base(rocketMovementController,asteroidCollider)
        {
            
        }
        
        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                _screenBounds.x + _asteroidCollider.radius * 2,
                -_screenBounds.y,
                _rocketPosition.z);
               
        }

       
    }
}