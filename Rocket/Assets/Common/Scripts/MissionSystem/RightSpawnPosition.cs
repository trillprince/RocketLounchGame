using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpawnPosition : SpawnPosition,ISpawnPosition
    {

        public RightSpawnPosition(RocketMovementController rocketMovementController,MeshCollider meshCollider): base(rocketMovementController,meshCollider)
        {
           
        }   

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                (- _screenBounds.x + _rocketPosition.x) / 2 + _meshCollider.bounds.size.x,
                -_screenBounds.y + _meshCollider.bounds.size.y / 2,
                _rocketPosition.z);
        }
    }
}