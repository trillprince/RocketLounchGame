using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public interface ISatellite
{
    public void Move(Action<ISatellite> action);
    public Transform GetTransform();
    public MeshCollider GetMeshCollider();

    public GameObject GetGameObject();
    public SatelliteState SatelliteState { get; set; }

    void Constructor(RocketMovementController rocketMovementController);
}