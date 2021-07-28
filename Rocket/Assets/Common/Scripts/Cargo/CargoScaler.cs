using System;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoScaler : MonoBehaviour
    {
        private float _scaleDownSpeed = 10;
        private Vector3 _cargoScale;
        private Vector3 _minScale;


        public void InitScale(Vector3 rocketScale)
        {
            Vector3 newScale = new Vector3(
                rocketScale.x * transform.localScale.x,
                rocketScale.y * transform.localScale.y, 
                rocketScale.z * transform.localScale.z);
            
             _cargoScale = newScale;
        }

        private void Awake()
        {
            _minScale = new Vector3(_cargoScale.x / 5, _cargoScale.y / 5, _cargoScale.z / 5);
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