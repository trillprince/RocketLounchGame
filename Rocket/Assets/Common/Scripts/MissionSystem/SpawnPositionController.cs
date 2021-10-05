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
            MeshCollider meshCollider)
        {
            SpawnPositions = new ISpawnPosition[]
            {
                leftSpawnPosition,
                new LeftMidSpawnPosition(rocketMovementController,
                    leftSpawnPosition,
                    middleSpawnPosition,meshCollider),
                rightSpawnPosition,
                new RightMidSpawnPosition(rocketMovementController,
                    rightSpawnPosition, 
                    middleSpawnPosition,meshCollider),
                middleSpawnPosition
            };
        }
    }
}