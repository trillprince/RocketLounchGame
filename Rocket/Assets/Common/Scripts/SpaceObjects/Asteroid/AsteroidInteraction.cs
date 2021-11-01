using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class AsteroidInteraction
    {
        private readonly RocketHealth _rocketHealth;
        private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
        private readonly ISpaceObject _spaceObject;

        public AsteroidInteraction(RocketHealth rocketHealth,ISpaceObjectLifeCycle spaceObjectLifeCycle,ISpaceObject spaceObject)
        {
            _rocketHealth = rocketHealth;
            _spaceObjectLifeCycle = spaceObjectLifeCycle;
            _spaceObject = spaceObject;
        }

        public void Interact()
        {
            _rocketHealth.DamageRocket(1);
            _spaceObjectLifeCycle.Dispose(_spaceObject);
        }
    }
}