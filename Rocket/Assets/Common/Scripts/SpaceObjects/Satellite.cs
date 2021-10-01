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
        private SatelliteColor _satelliteColor;
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
            _satelliteColor = new SatelliteColor(GetComponent<MeshRenderer>());
            _satelliteDelivery = new SatelliteDelivery(_satelliteColor,satelliteController,gameLoopController);
            _satelliteStateOnScreen = new SatelliteStateOnScreen(GetComponent<MeshCollider>(), 
                transform,
                _satelliteColor,
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
