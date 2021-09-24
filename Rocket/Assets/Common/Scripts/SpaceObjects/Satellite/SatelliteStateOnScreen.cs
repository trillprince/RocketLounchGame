using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteStateOnScreen: StateOnScreenPosition
    {
        private StateOnScreen _finalStateOnScreen;
        private StateOnScreen _currentStateOnScreen;
        public bool CargoDelivered { get; private set; } = false;
        private readonly SatelliteColor _satelliteColor;
        private SatelliteDelivery _satelliteDelivery;

        public SatelliteStateOnScreen(MeshCollider meshCollider,
            Transform transform,
            SatelliteColor satelliteColor,
            ISpaceObjectController spaceObjectController,
            GameLoopController gameLoopController,SatelliteDelivery satelliteDelivery): base(meshCollider,transform,spaceObjectController,gameLoopController)
        {
            _satelliteColor = satelliteColor;
            _satelliteDelivery = satelliteDelivery;
        }

        protected override void OnStateChange(StateOnScreen state)
        {
            switch (state)
            {
                case StateOnScreen.UpperRed:
                    _satelliteColor.SetColor(Color.red);
                    break;
                case StateOnScreen.Yellow:
                    _satelliteColor.SetColor(Color.yellow);
                    break;
                case StateOnScreen.Green:
                    _satelliteColor.SetColor(Color.green);
                    break;
                case StateOnScreen.LowerRed:
                    _satelliteColor.SetColor(Color.red);
                    SpaceObjectController.ScopeToNextObject();
                    break;
                case StateOnScreen.DisposeZone:
                    _satelliteDelivery.DisableSatelliteDrop();
                    SpaceObjectController.DisposeLastObject();
                    break;
            }
        }
    }
}