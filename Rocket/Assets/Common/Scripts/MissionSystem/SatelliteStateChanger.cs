using System;
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
        public void Execute()
        {
            CargoDeliveryOnInput();
        }

        private void CargoDeliveryOnInput()
        {
            if (_inputListener.InputLeftSide &&
                _leftSatelliteController.SatellitesExist() &&
                !_leftSatelliteController.LeftScopedSatellite.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_leftSatelliteController.LeftScopedSatellite);
                Debug.Log("input 1");
            }
            else if(_inputListener.InputRightSide &&
                    _rightSatelliteController.SatellitesExist() &&
                    !_rightSatelliteController.RightScopedSatellite.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_rightSatelliteController.RightScopedSatellite);
                Debug.Log("input 2");

            }
        }

        public void DisableInput()
        {
            _inputListener.DisableInput();
        }
    }
}