using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoController : MonoBehaviour
    {
        private ISatellite _spaceObject;
        private CargoMovement _cargoMovement;
        private CargoScaler _cargoScaler;

        public void Constructor(ISatellite spaceObject,ObjectPool objectPool)
        {
            _spaceObject = spaceObject;
            _cargoMovement = new CargoMovement(transform,_spaceObject,objectPool,gameObject);
        }

        private void FixedUpdate()
        {
            _cargoMovement.Execute();
        }

        private void ChangeSatelliteState()
        {
            _spaceObject.SetFinalDeliveryStatus();
        }
    }
}