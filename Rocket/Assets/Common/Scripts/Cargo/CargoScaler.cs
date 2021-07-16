using System;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoScaler : MonoBehaviour
    {
        private float _scaleDownSpeed = 25f;
        private Vector3 _cargoScale;
        private Vector3 _minScale;


        private void Awake()
        {
            _cargoScale = transform.localScale;
            _minScale = new Vector3(_cargoScale.x/2, _cargoScale.y/2, _cargoScale.z/2);
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
