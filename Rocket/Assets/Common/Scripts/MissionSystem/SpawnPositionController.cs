using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpawnPositionController
    {
        public ISpawnPosition[] SpawnPositions { get; }

        public SpawnPositionController(RocketMovementController rocketMovementController, 
            LeftSpawnPosition leftSpawnPosition,
            RightSpawnPosition rightSpawnPosition,
            MiddleSpawnPosition middleSpawnPosition,
            SphereCollider collider)
        {
            SpawnPositions = new ISpawnPosition[]
            {
                leftSpawnPosition,
                new LeftMidSpawnPosition(rocketMovementController,
                    leftSpawnPosition,
                    middleSpawnPosition,collider),
                rightSpawnPosition,
                new RightMidSpawnPosition(rocketMovementController,
                    rightSpawnPosition, 
                    middleSpawnPosition,collider),
                middleSpawnPosition
            };
        }
    }
}