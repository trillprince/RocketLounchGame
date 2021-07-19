using System;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoScaler : MonoBehaviour
    {
        private float _scaleDownSpeed = 10;
        private Vector3 _cargoScale;
        private Vector3 _minScale;


        private void Awake()
        {
            _cargoScale = transform.localScale;
            _minScale = new Vector3(_cargoScale.x/10, _cargoScale.y/10, _cargoScale.z/10);
        }

        void ScaleDown()
        {
            var xScale = Mathf.Lerp(_cargoScale.x, _minScale.x, _scaleDownSpeed * Time.deltaTime);
            var yScale = Mathf.Lerp(_cargoScale.y, _minScale.y, _scaleDownSpeed * Time.deltaTime);
            var zScale = Mathf.Lerp(_cargoScale.z, _minScale.y, _scaleDownSpeed * Time.deltaTime);
            transform.localScale = new Vector3(xScale, yScale, zScale);
        }

        private void FixedUpdate()
        {
            ScaleDown();
        }
        
    }
}
