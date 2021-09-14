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

        void SetCargo(GameObject cargo)
        {
            _currentCargo = cargo;
        }

        void DropCargo()
        {
            if (_currentCargo != null)
            {
                var cargo = Instantiate(_currentCargo, transform.position, Quaternion.identity);
                cargo.GetComponent<CargoScaler>().InitScale(transform.localScale);
            }
        }
    }
}
