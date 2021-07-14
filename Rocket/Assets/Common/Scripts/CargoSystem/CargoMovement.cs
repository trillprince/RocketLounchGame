using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.CargoSystem
{
    public class CargoMovement : MonoBehaviour
    {
        private RocketMovement _rocketMovement;
        private int _smoothness = 8;
        private float _timeTillDestroy = 3f;
        private float _rotateSpeed = 30f;

        private void Start()
        {
            StartCoroutine(WaitTillDestroy());
        }

        IEnumerator WaitTillDestroy()
        {
            yield return new WaitForSeconds(_timeTillDestroy);
        }

        public void InitCargo(RocketMovement rocketMovement)
        {
            _rocketMovement = rocketMovement;
        }
        void CargoMove()
        {
            transform.Translate(Vector3.down * _rocketMovement.RocketSpeed/_smoothness * Time.deltaTime);
        }

        void CargoRotate()
        {
            transform.RotateAround(transform.localPosition, Random.rotation.eulerAngles, _rotateSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            CargoRotate();
            CargoMove();
        }

    }
}
