using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.CargoSystem
{
    public class CargoMovement : MonoBehaviour
    {
        private RocketMovement _rocketMovement;
        private int _smoothness = 10;
        private float _timeTillDestroy = 3f;
        private float _rotateSpeed = 30f;
        private int _yRot;
        private int _zRot;
        
        private void Start()
        {
            SetRandomRot();
        }


        public void InitCargo(RocketMovement rocketMovement)
        {
            _rocketMovement = rocketMovement;
        }
        void CargoMove()
        {
            transform.Translate(-_rocketMovement.GetRocketDirection() * _rocketMovement.RocketSpeed/_smoothness * Time.deltaTime);
        }

        void CargoRotate()
        {
            transform.Rotate(
                transform.rotation.x,
                _yRot * _rotateSpeed * Time.deltaTime, 
                _zRot * _rotateSpeed * Time.deltaTime
                );
        }

        private void FixedUpdate()
        {
            CargoMove();
            CargoRotate();
        }

        private void SetRandomRot()
        {
            _yRot  = Random.Range(-1, 2);
            _zRot = Random.Range(-1, 2);
        }

    }
}
