using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteStateChanger: IUpdatable
    {
        private readonly InputListener _inputListener;
        private readonly LeftSatelliteController _leftSatelliteController;
        private readonly RightSatelliteController _rightSatelliteController;
        private readonly RocketCargo _rocketCargo;

        public SatelliteStateChanger(InputListener inputListener, 
            LeftSatelliteController leftSatelliteController
            ,RightSatelliteController rightSatelliteController, 
            RocketCargo rocketCargo)
        {
            _inputListener = inputListener;
            _leftSatelliteController = leftSatelliteController;
            _rightSatelliteController = rightSatelliteController;
            _rocketCargo = rocketCargo;
        }
        public void Update()
        {
            CargoDeliveryOnInput();
        }

        private void CargoDeliveryOnInput()
        {
            if (_inputListener.InputLeftSide && _leftSatelliteController.SatellitesExist())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_leftSatelliteController.leftScopedSatellite);
                _leftSatelliteController.DisposeScopedSatellite();
                Debug.Log("left input");
            }
            else if(_inputListener.InputRightSide && _rightSatelliteController.SatellitesExist())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_rightSatelliteController.rightScopedSatellite);
                _rightSatelliteController.DisposeScopedSatellite();
                Debug.Log("right input");
            }
        }
    }
}