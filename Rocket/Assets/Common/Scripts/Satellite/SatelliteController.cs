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
        private SatelliteColor _satelliteColor;
        
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public Transform GetTransform()
        {
            return transform;
        }

        public void Constructor(RocketMovementController rocketMovementController,
            GameStateController gameStateController, 
            ISatelliteController satelliteController,
            GameLoopController gameLoopController)
        {
            _satelliteMove = new SatelliteMove(rocketMovementController, transform);
            _satelliteColor = new SatelliteColor(GetComponent<MeshRenderer>());
            _satelliteDelivery = new SatelliteDelivery(GetComponent<MeshCollider>(), 
                transform,
                _satelliteColor,
                satelliteController,
                gameLoopController
            );
        }

        public void SetFinalDeliveryStatus()
        {
            _satelliteDelivery.SetFinalDeliveryStatus();
        }

        public bool HasCargo()
        {
            return _satelliteDelivery.CargoDelivered;
        }

        public void Execute()
        {
            _satelliteMove.Move();
            _satelliteDelivery.StateCheck();
        }
    }
}
