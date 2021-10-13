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
            MiddleSpawnPosition middleSpawnPosition)
        {
            SpawnPositions = new ISpawnPosition[]
            {
                leftSpawnPosition,
                new LeftMidSpawnPosition(rocketMovement,
                    leftSpawnPosition,
                    middleSpawnPosition),
                rightSpawnPosition,
                new RightMidSpawnPosition(rocketMovement,
                    rightSpawnPosition, 
                    middleSpawnPosition),
                middleSpawnPosition
            };
        }
    }
}