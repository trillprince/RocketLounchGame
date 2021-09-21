using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteController : MonoBehaviour,ISatellite
    {
        private SatelliteDelivery _satelliteDelivery;
        private SatelliteMove _satelliteMove;
        
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public Transform GetTransform()
        {
            return transform;
        }

        public void Constructor(RocketMovementController rocketMovementController, Action  onDispose)
        {
            _satelliteDelivery = new SatelliteDelivery(GetComponent<MeshCollider>(), transform, onDispose);
            _satelliteMove = new SatelliteMove(rocketMovementController, transform);
        }

        public void SetFinalDeliveryStatus()
        {
            _satelliteDelivery.SetFinalDeliveryStatus();
        }

        public void Execute()
        {
            _satelliteMove.Move();
            _satelliteDelivery.StateCheck();
        }
    }
}
