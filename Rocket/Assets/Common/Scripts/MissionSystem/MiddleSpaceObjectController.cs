﻿using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpaceObjectController: SpaceObjectController
    {
        
        public MiddleSpaceObjectController(
            SpaceObjectFactory spaceObjectFactory,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController,
         Queue<ISpaceObject> spaceObjects): base(spaceObjectFactory,rocketMovementController,gameStateController,gameLoopController,spaceObjects)
        {

        }
        
        
        
 
    }
}