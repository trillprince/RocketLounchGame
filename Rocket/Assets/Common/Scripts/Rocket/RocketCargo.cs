using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI.InGame;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketCargo : MonoBehaviour
    {

        private GameObject _currentCargo;
        
        private void OnEnable()
        {
            MissionManager.SetCargo += SetCargo;
            DropCargoButton.CargoDropped += DropCargo;
        }

        private void OnDisable()
        {
            MissionManager.SetCargo -= SetCargo;
            DropCargoButton.CargoDropped -= DropCargo;
        }
        
        void SetCargo(GameObject cargo)
        {
            _currentCargo = cargo;
        }

        void DropCargo()
        {
            Instantiate(_currentCargo, transform.position, Quaternion.identity);
        }
    }
}
