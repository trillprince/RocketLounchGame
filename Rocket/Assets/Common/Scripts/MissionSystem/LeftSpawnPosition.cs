using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public LeftSpawnPosition(RocketMovement rocketMovement): base(rocketMovement)
        {
            
        }
        
        public Vector3 GetSpawnPosition(Collider collider)
        {
            return new Vector3(
                _screenBounds.x + collider.transform.localScale.x,
                -_screenBounds.y,
                _rocketPosition.z);
               
        }

       
    }
}