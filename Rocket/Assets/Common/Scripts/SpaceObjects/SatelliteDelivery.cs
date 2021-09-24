using Common.Scripts.MissionSystem;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects
{
    public class SatelliteDelivery
    {
        private readonly SatelliteColor _satelliteColor;
        private readonly ISpaceObjectController _spaceObjectController;
        private StateOnScreen _finalStateOnScreen;
        private StateOnScreen _currentStateOnScreen;
        private GameLoopController _gameLoopController;
        public bool CargoDelivered { get; private set; } = false;

        public SatelliteDelivery(SatelliteColor satelliteColor,ISpaceObjectController spaceObjectController,GameLoopController gameLoopController)
        {
            _satelliteColor = satelliteColor;
            _spaceObjectController = spaceObjectController;
            _gameLoopController = gameLoopController;
        }
        public void DisableSatelliteDrop()
        {
            if (!CargoDelivered)
            {
                _gameLoopController.DisableSatelliteDrop();
            }
        }
        
        public void SetFinalDeliveryStatus()
        {
            Debug.Log("set final");
            CargoDelivered = true;
            _satelliteColor.SetFinalColor();
            _spaceObjectController.ScopeToNextObject();
            _finalStateOnScreen = _currentStateOnScreen;
        }
    }
}