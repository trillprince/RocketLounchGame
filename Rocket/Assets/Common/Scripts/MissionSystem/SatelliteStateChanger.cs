using System;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    public class SatelliteStateChanger: IUpdatable
    {
        private readonly InputListener _inputListener;
        private readonly LeftSpaceObjectController _leftSpaceObjectController;
        private readonly RightSpaceObjectController _rightSpaceObjectController;
        private readonly RocketCargo _rocketCargo;
        private bool _isEnabled;

        public SatelliteStateChanger(InputListener inputListener, 
            LeftSpaceObjectController leftSpaceObjectController
            ,RightSpaceObjectController rightSpaceObjectController, 
            RocketCargo rocketCargo)
        {
            _inputListener = inputListener;
            _leftSpaceObjectController = leftSpaceObjectController;
            _rightSpaceObjectController = rightSpaceObjectController;
            _rocketCargo = rocketCargo;
        }
        public void Execute()
        {
            if (_isEnabled)
            {
                CargoDeliveryOnInput();
            }
        }

        private void CargoDeliveryOnInput()
        {
            if (_inputListener.InputLeftSide &&
                _leftSpaceObjectController.SatellitesExist() &&
                !_leftSpaceObjectController.LeftScopedSpaceObject.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_leftSpaceObjectController.LeftScopedSpaceObject);
                Debug.Log("input 1");
            }
            else if(_inputListener.InputRightSide &&
                    _rightSpaceObjectController.SatellitesExist() &&
                    !_rightSpaceObjectController.RightScopedSpaceObject.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_rightSpaceObjectController.RightScopedSpaceObject);
                Debug.Log("input 2");
            }
        }

        public void DisableInput()
        {
            _inputListener.DisableInput();
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }
    }
}