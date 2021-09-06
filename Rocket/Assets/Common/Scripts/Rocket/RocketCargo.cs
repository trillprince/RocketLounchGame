using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketCargo : MonoBehaviour
    {
        private GameObject _currentCargo;

        private void OnEnable()
        {
            DropStatusController.SetCargo += SetCargo;
            CargoDropController.CargoDropping += DropCargo;
        }

        private void OnDisable()
        {
            DropStatusController.SetCargo -= SetCargo;
            CargoDropController.CargoDropping -= DropCargo;
        }
        
        void SetCargo(GameObject cargo)
        {
            _currentCargo = cargo;
            Debug.Log($"SetCargo {_currentCargo}");
        }

        void DropCargo()
        {
            Debug.Log($"current cargo count {_currentCargo}");
            if (_currentCargo != null)
            {
                var cargo = Instantiate(_currentCargo, transform.position, Quaternion.identity);
                cargo.GetComponent<CargoScaler>().InitScale(transform.localScale);
            }
        }
    }
}
