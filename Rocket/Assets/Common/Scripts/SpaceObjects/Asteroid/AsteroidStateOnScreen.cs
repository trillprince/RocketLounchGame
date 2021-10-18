using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class AsteroidStateOnScreen: StateOnScreenPosition
    {
        public bool CargoDelivered { get; private set; } = false;
        private AsteroidDelivery _asteroidDelivery;

        public AsteroidStateOnScreen(
            Transform transform,
            ISpaceObjectLifeCycle spaceObjectLifeCycle,
            AsteroidDelivery asteroidDelivery,
            ISpaceObject spaceObject): base(transform,spaceObjectLifeCycle,spaceObject)
        {
            _asteroidDelivery = asteroidDelivery;
        }

    }
}