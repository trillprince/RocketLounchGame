﻿using System;
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
            CargoDeliveryOnInput();
        }

        private void CargoDeliveryOnInput()
        {
            if (_inputListener.InputLeftSide &&
                _leftSpaceObjectController.SatellitesExist() &&
                !_leftSpaceObjectController.LeftScopedSatellite.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_leftSpaceObjectController.LeftScopedSatellite);
                Debug.Log("input 1");
            }
            else if(_inputListener.InputRightSide &&
                    _rightSpaceObjectController.SatellitesExist() &&
                    !_rightSpaceObjectController.RightScopedSatellite.HasCargo())
            {
                _inputListener.OnTouchEnd();
                _rocketCargo.DropCargo(_rightSpaceObjectController.RightScopedSatellite);
                Debug.Log("input 2");

            }
        }

        public void DisableInput()
        {
            _inputListener.DisableInput();
        }
    }
}