using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : MonoBehaviour
    {
        private RocketMovementController _onTouchRocketMove;
        private int _smoothness = 7;
        private float _rotateSpeed = 50f;
        private int _zRot;

        private void Start()
        {
            SetRandomRot();
        }


        public void InitCargo(RocketMovementController rocketMovement)
        {
            _onTouchRocketMove = rocketMovement;
        }
        
        void CargoMove()
        {
            transform.Translate(-_onTouchRocketMove.GetRocketDirection() * _onTouchRocketMove.RocketSpeed/_smoothness * Time.deltaTime);
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
