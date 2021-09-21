using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Common.Scripts.Rocket
{
    public class RocketCargo : MonoBehaviour
    {
        [SerializeField] private GameObject _cargoPrefab;
        private ObjectPool _objectPool;
        private CargoController _currentCargoController;
        private GameObject _currentCargo;

        [Inject]
        private void Constructor(ObjectPoolStorage objectPoolStorage)
        {
            _objectPool = objectPoolStorage.GetPool(_cargoPrefab);
        }
        
        public void DropCargo (ISatellite satellite)
        {
            var cargo = _objectPool.Pop(transform.position);
            _currentCargoController = cargo.GetComponent<CargoController>();
            _currentCargoController.Constructor(satellite,_objectPool);
        }

        private void DisposeCargo()
        {
            if (_currentCargo != null)
            {
                _objectPool.Push(_currentCargo);
            }
        }
    }
}
