using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public interface ISatellite
{
    public void Constructor(RocketMovementController rocketMovementController);
    public Transform Move();
    public MeshCollider GetMeshCollider();
}