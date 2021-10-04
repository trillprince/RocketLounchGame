using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class Satellite : MonoBehaviour, ISpaceObject
    {
        private SatelliteStateOnScreen _satelliteStateOnScreen;
        private SatelliteMove _satelliteMove;
        private SatelliteDelivery _satelliteDelivery;

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
            ISpaceObjectController satelliteController,
            GameLoopController gameLoopController)
        {
            
            _satelliteMove = new SatelliteMove(rocketMovementController, transform);
            _satelliteDelivery = new SatelliteDelivery(satelliteController,gameLoopController);
            _satelliteStateOnScreen = new SatelliteStateOnScreen(GetComponent<MeshCollider>(), 
                transform,
                satelliteController,
                gameLoopController,
                _satelliteDelivery
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
            _satelliteStateOnScreen.StateCheck();
        }
        
    }
}
