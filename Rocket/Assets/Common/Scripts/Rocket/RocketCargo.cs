using System;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketCargo : MonoBehaviour
    {
        [SerializeField] private GameObject _cargoPrefab;
        private ObjectPool _objectPool;
        private CargoController _currentCargoController;
        private GameObject _currentCargo;
        private SatelliteCount _satelliteCount;
        public event Action OnCargoDrop;

        [Inject]
        private void Constructor(ObjectPoolStorage objectPoolStorage,SatelliteCount satelliteCount)
        {
            _satelliteCount = satelliteCount;
            _objectPool = objectPoolStorage.GetPool(_cargoPrefab);
        }

        public void DropCargo(ISatellite satellite)
        {
            OnCargoDrop?.Invoke();
            _satelliteCount.AddSatellite();
            satellite.SetFinalDeliveryStatus();
            var cargo = _objectPool.Pop(transform.position);
            _currentCargoController = cargo.GetComponent<CargoController>();
            _currentCargoController.Constructor(satellite, _objectPool);
        }
    }
}