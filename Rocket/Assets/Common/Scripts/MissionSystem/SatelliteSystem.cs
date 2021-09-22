using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteSystem: ISatelliteSystem
    {
        private readonly ISatelliteController _leftSatelliteController;
        private readonly ISatelliteController _rightSatelliteController;
        private readonly InputListener _inputListener;
        private readonly SatelliteStateChanger _satelliteStateChanger;

        public SatelliteSystem
        (
            ISatelliteController leftSatelliteController,
            ISatelliteController rightSatelliteController,
            InputListener inputListener,
            SatelliteStateChanger satelliteStateChanger
        )
        {
            _leftSatelliteController = leftSatelliteController;
            _rightSatelliteController = rightSatelliteController;
            _inputListener = inputListener;
            _satelliteStateChanger = satelliteStateChanger;
        }

        public void SpawnRandomSideSatellite()
        {
            var range = Random.Range(-2, 2);
            if (range < 0)
            {
                _leftSatelliteController.Spawn();
            }
            else
            {
                _rightSatelliteController.Spawn();
            }
        }

        public void Execute()
        {
            _leftSatelliteController.Execute();
            _rightSatelliteController.Execute();
            _satelliteStateChanger.Execute();
        }
    }
}