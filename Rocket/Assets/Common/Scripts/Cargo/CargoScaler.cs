using System;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoScaler : IUpdatable
    {
        private float _scaleDownSpeed = 10;
        private Transform _transform;
        private Vector3 _cargoScale;
        private Vector3 _minScale;

        public CargoScaler(Transform transform)
        {
            _transform = transform;
            _minScale = new Vector3(_cargoScale.x / 5, _cargoScale.y / 5, _cargoScale.z / 5);
        }


        public void InitScale(Vector3 rocketScale)
        {
            Vector3 newScale = new Vector3(
                rocketScale.x * _transform.localScale.x,
                rocketScale.y * _transform.localScale.y, 
                rocketScale.z * _transform.localScale.z);
            
             _cargoScale = newScale;
        }

        void ScaleDown()
        {
            var xScale = Mathf.Lerp(_cargoScale.x, _minScale.x, _scaleDownSpeed * Time.deltaTime);
            var yScale = Mathf.Lerp(_cargoScale.y, _minScale.y, _scaleDownSpeed * Time.deltaTime);
            var zScale = Mathf.Lerp(_cargoScale.z, _minScale.y, _scaleDownSpeed * Time.deltaTime);
            _transform.localScale = new Vector3(xScale, yScale, zScale);
        }

        public void Execute()
        {
            ScaleDown();
        }
    }
}