using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public LeftSpawnPosition(RocketMovement rocketMovement, Collider asteroidCollider): base(rocketMovement,asteroidCollider)
        {
            
        }
        
        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                _screenBounds.x + _asteroidCollider.transform.localScale.x,
                -_screenBounds.y,
                _rocketPosition.z);
               
        }

       
    }
}