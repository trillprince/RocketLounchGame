using System;
using System.Collections.Generic;
using Common.Scripts.CargoSystem;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI.InGame;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketCargo : MonoBehaviour
    {

        private GameObject _currentCargo;
        private RocketMovement _rocketMovement;

        private void Awake()
        {
            _rocketMovement = GetComponent<RocketMovement>();
            Debug.Log(_rocketMovement);
        }

        private void OnEnable()
        {
            MissionManager.SetCargo += SetCargo;
            CargoDropListener.CargoDropped += DropCargo;
        }

        private void OnDisable()
        {
            MissionManager.SetCargo -= SetCargo;
            CargoDropListener.CargoDropped -= DropCargo;
        }
        
        void SetCargo(GameObject cargo)
        {
            _currentCargo = cargo;
        }

        void DropCargo()
        {
            var cargo = Instantiate(_currentCargo, transform.position, Quaternion.identity);
            cargo.GetComponent<CargoMovement>().InitCargo(_rocketMovement);
        }
    }
}
