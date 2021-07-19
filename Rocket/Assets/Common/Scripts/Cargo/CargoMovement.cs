using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : MonoBehaviour
    {
        private RocketMovement _rocketMovement;
        private int _smoothness = 7;
        private float _rotateSpeed = 50f;
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
                transform.rotation.y, 
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
            _zRot = Random.value < 0.5f ? -1 : 1;
        }
        

    }
}
