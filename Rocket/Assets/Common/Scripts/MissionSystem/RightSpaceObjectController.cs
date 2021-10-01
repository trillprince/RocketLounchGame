using System;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class RightSpaceObjectController: SpaceObjectController
    {

        public RightSpaceObjectController(
            SpaceObjectSpawner spaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController,
            Queue<ISpaceObject> spaceObjects): base(spaceObjectSpawner,rocketMovementController,gameStateController,gameLoopController,spaceObjects)
        {

        }
        
    }
}