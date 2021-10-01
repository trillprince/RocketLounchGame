using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public interface ISpaceObject: IUpdatable
{
    public void Constructor(RocketMovementController rocketMovementController,
        GameStateController gameStateController,ISpaceObjectController satelliteController,GameLoopController gameLoopController);
    
    public GameObject GetGameObject();

    public Transform GetTransform();
    

}

public enum StateOnScreen
{
    UpperRed,
    Yellow,
    Green,
    LowerRed,
    DisposeZone
}