using System;
using System.Collections.Generic;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class LeftSpaceObjectController: SpaceObjectController
    {
        public LeftSpaceObjectController(
            SpaceObjectSpawner spaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController,
            Queue<ISpaceObject> spaceObjects): base(spaceObjectSpawner,rocketMovementController,gameStateController,gameLoopController,spaceObjects)
        {
            
        }

       
    }

}