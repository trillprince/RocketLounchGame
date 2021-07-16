using System;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoScaler : MonoBehaviour
    {
        private float _scaleDownSpeed = 10f;
        private BGScroll _bgScroll;
        private float _zPosOfBg;
        private Vector3 _cargoScale;

        private void Awake()
        {
            _cargoScale = transform.localScale;
            _bgScroll = FindObjectOfType<BGScroll>();
            _zPosOfBg = _bgScroll.transform.position.z;
        }

        void ScaleDown()
        {
            float distance = _zPosOfBg - transform.position.z;
            var xscale = Mathf.Lerp(_cargoScale.x, _cargoScale.x/distance, _scaleDownSpeed * Time.deltaTime);
            var yscale = Mathf.Lerp(_cargoScale.y, _cargoScale.y/distance, _scaleDownSpeed * Time.deltaTime);
            var zscale = Mathf.Lerp(_cargoScale.z, _cargoScale.z/distance, _scaleDownSpeed * Time.deltaTime);
            transform.localScale = new Vector3(xscale, yscale, zscale);
        }

        private void FixedUpdate()
        {
            ScaleDown();
        }
        
    }
}
