using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public interface ISatellite
{
    public void Move();
    public void StateCheck();

    public GameObject GetGameObject();

    public Transform GetTransform();
    

    void Constructor(RocketMovementController rocketMovementController, Action onDispose);
}