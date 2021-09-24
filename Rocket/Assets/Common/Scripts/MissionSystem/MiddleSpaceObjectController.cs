using Common.Scripts.Rocket;

namespace Common.Scripts.MissionSystem
{
    public class MiddleSpaceObjectController: ISpaceObjectController
    {
        private readonly ISpaceObjectSpawner _rightSpaceObjectSpawner;
        private readonly RocketMovementController _rocketMovementController;
        private readonly GameStateController _gameStateController;
        private readonly GameLoopController _gameLoopController;

        public MiddleSpaceObjectController(
            ISpaceObjectSpawner rightSpaceObjectSpawner,
            RocketMovementController rocketMovementController,
            GameStateController gameStateController,
            GameLoopController gameLoopController)
        {
            _rightSpaceObjectSpawner = rightSpaceObjectSpawner;
            _rocketMovementController = rocketMovementController;
            _gameStateController = gameStateController;
            _gameLoopController = gameLoopController;
        }
        public void Spawn()
        {
            
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void DisposeLastSatellite()
        {
            throw new System.NotImplementedException();
        }

        public void DisposeAll()
        {
            throw new System.NotImplementedException();
        }

        public void ScopeToNextSatellite()
        {
            throw new System.NotImplementedException();
        }
    }
}