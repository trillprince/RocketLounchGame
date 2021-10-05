using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class AsteroidStateOnScreen: StateOnScreenPosition
    {
        private StateOnScreen _finalStateOnScreen;
        private StateOnScreen _currentStateOnScreen;
        public bool CargoDelivered { get; private set; } = false;
        private AsteroidDelivery _asteroidDelivery;

        public AsteroidStateOnScreen(MeshCollider meshCollider,
            Transform transform,
            ISpaceObjectController spaceObjectController,AsteroidDelivery asteroidDelivery): base(meshCollider,transform,spaceObjectController)
        {
            _asteroidDelivery = asteroidDelivery;
        }

        protected override void OnStateChange(StateOnScreen state)
        {
            switch (state)
            {
                case StateOnScreen.UpperRed:
                    break;
                case StateOnScreen.Yellow:
                    break;
                case StateOnScreen.Green:
                    break;
                case StateOnScreen.LowerRed:
                    break;
                case StateOnScreen.DisposeZone:
                    SpaceObjectController.DisposeLastObject();
                    // _satelliteDelivery.CheckForDeliveryExistance();
                    break;
            }
        }
    }
}