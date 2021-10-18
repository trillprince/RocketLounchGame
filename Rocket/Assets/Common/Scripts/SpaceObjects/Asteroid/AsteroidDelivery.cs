using Common.Scripts.MissionSystem;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class AsteroidDelivery
    {
        private readonly ISpaceObjectLifeCycle _spaceObjectLifeCycle;
        private GameLoopController _gameLoopController;
        public bool CargoDelivered { get; private set; } = false;

        public AsteroidDelivery(ISpaceObjectLifeCycle spaceObjectLifeCycle,GameLoopController gameLoopController)
        {
            _spaceObjectLifeCycle = spaceObjectLifeCycle;
            _gameLoopController = gameLoopController;
        }
    }
}