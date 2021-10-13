using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public RightSpawnPosition(RocketMovement rocketMovement): base(rocketMovement)
        {
           
        }   

        public Vector3 GetSpawnPosition(Collider collider)
        {
            return new Vector3(
                -_screenBounds.x - collider.transform.localScale.x,
                -_screenBounds.y,
                _rocketPosition.z);
        }
    }
}