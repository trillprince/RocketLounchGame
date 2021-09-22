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

        [Inject]
        private void Constructor(ObjectPoolStorage objectPoolStorage)
        {
            _objectPool = objectPoolStorage.GetPool(_cargoPrefab);
        }
        
        public void DropCargo (ISatellite satellite)
        {
            if (!satellite.HasCargo())
            {
                satellite.SetFinalDeliveryStatus();
                var cargo = _objectPool.Pop(transform.position);
                _currentCargoController = cargo.GetComponent<CargoController>();
                _currentCargoController.Constructor(satellite,_objectPool);
            }
        }

    }
}
