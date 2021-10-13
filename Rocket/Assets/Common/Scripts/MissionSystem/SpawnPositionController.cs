using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpawnPositionController
    {
        public ISpawnPosition[] SpawnPositions { get; }

        public SpawnPositionController(RocketMovement rocketMovement, 
            LeftSpawnPosition leftSpawnPosition,
            RightSpawnPosition rightSpawnPosition,
            MiddleSpawnPosition middleSpawnPosition,
            Collider collider)
        {
            SpawnPositions = new ISpawnPosition[]
            {
                leftSpawnPosition,
                new LeftMidSpawnPosition(rocketMovement,
                    leftSpawnPosition,
                    middleSpawnPosition,collider),
                rightSpawnPosition,
                new RightMidSpawnPosition(rocketMovement,
                    rightSpawnPosition, 
                    middleSpawnPosition,collider),
                middleSpawnPosition
            };
        }
    }
}