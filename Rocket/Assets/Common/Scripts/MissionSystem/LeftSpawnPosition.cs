using Common.Scripts.Camera;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public LeftSpawnPosition(RocketMovementController rocketMovementController): base(rocketMovementController)
        {
            
        }
        
        public Vector3 GetSpawnPosition(MeshCollider meshCollider)
        {
            return new Vector3(
                (_screenBounds.x - _rocketPosition.x) / 2 - meshCollider.bounds.size.x,
                -_screenBounds.y + meshCollider.bounds.size.y / 2,
                _rocketPosition.z);
               
        }

       
    }
}