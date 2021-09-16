using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

public class BasicSatellite : MonoBehaviour,ISatellite
{
    private RocketMovementController _rocketMoveController;
    private float _moveSmoothness = 10f;
    private MeshCollider _meshCollider;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void Constructor(RocketMovementController rocketMovementController)
    {
        _rocketMoveController = rocketMovementController;
    }

    public Transform Move()
    {
        transform.Translate((-_rocketMoveController.GetRocketDirection()*_rocketMoveController.GetRocketSpeed())/_moveSmoothness * Time.deltaTime);
        return transform;
    }

    public MeshCollider GetMeshCollider()
    {
        return _meshCollider;
    }
}
