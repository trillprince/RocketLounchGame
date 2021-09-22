using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public interface ISatellite: IUpdatable
{
    
    public GameObject GetGameObject();

    public Transform GetTransform();

    public void Constructor(RocketMovementController rocketMovementController,
        GameStateController gameStateController,ISatelliteController satelliteController,GameLoopController gameLoopController);

    public void SetFinalDeliveryStatus();
    bool HasCargo();
}

public enum DeliveryStatus
{
    UpperRed,
    Yellow,
    Green,
    Black
}