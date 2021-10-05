using Common.Scripts.MissionSystem;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class AsteroidDelivery
    {
        private readonly ISpaceObjectController _spaceObjectController;
        private GameLoopController _gameLoopController;
        public bool CargoDelivered { get; private set; } = false;

        public AsteroidDelivery(ISpaceObjectController spaceObjectController,GameLoopController gameLoopController)
        {
            _spaceObjectController = spaceObjectController;
            _gameLoopController = gameLoopController;
        }
    }
}