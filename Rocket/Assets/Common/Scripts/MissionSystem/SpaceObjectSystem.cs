using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SpaceObjectSystem : ISpaceObjectSystem
    {
        private readonly ISpaceObjectController _leftSpaceObjectController;
        private readonly ISpaceObjectController _rightSpaceObjectController;
        private readonly InputListener _inputListener;
        private readonly SatelliteStateChanger _satelliteStateChanger;
        private bool _satelliteSystemActive = true;
        private ISpaceObjectController[] _spaceObjectControllers;

        public SpaceObjectSystem
        (
            ISpaceObjectController leftSpaceObjectController,
            ISpaceObjectController rightSpaceObjectController,
            InputListener inputListener,
            SatelliteStateChanger satelliteStateChanger
        )
        {
            _leftSpaceObjectController = leftSpaceObjectController;
            _rightSpaceObjectController = rightSpaceObjectController;
            _inputListener = inputListener;
            _satelliteStateChanger = satelliteStateChanger;
        }

        public void SpawnRandomSideSatellite()
        {
            var range = Random.Range(-2, 2);
            if (range < 0)
            {
                _leftSpaceObjectController.Spawn();
            }
            else
            {
                _rightSpaceObjectController.Spawn();
            }
        }

        public void Execute()
        {
            if (_satelliteSystemActive)
            {
                _leftSpaceObjectController.Execute();
                _rightSpaceObjectController.Execute();
                _satelliteStateChanger.Execute();
            }
        }

        public void OnSystemDisable()
        {
            _satelliteSystemActive = false;
            
            _leftSpaceObjectController.DisposeAll();
            _rightSpaceObjectController.DisposeAll();
            _satelliteStateChanger.DisableInput();
        }
    }
}