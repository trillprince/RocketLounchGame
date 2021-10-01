using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpaceObjectController: SpaceObjectController
    {
        
        public MiddleSpaceObjectController(
            SpaceObjectSpawner spaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController,
         Queue<ISpaceObject> spaceObjects): base(spaceObjectSpawner,rocketMovementController,gameStateController,gameLoopController,spaceObjects)
        {

        }
        
        
        
 
    }
}