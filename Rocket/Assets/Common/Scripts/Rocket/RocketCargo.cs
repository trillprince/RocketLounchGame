using System;
using Common.Scripts.Cargo;
using Common.Scripts.MissionSystem;
using Common.Scripts.SpaceObjects;
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
        public event Action OnCargoDrop;

        [Inject]
        private void Constructor(ObjectPoolStorage objectPoolStorage)
        {
            _objectPool = objectPoolStorage.GetPool(_cargoPrefab);
        }

        public void DropCargo(ISpaceObject spaceObject)
        {
            OnCargoDrop?.Invoke();
            var cargo = _objectPool.Pop(transform.position);
            _currentCargoController = cargo.GetComponent<CargoController>();
            _currentCargoController.Constructor(spaceObject, _objectPool);
        }
    }
}