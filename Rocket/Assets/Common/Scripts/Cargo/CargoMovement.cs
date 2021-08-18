using Common.Scripts.Rocket;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.Cargo
{
    public class CargoMovement : MonoBehaviour
    {
        private MovementTypeSwitcher _onTouchMovementMove;
        private int _smoothness = 7;
        private float _rotateSpeed = 50f;
        private int _zRot;

        private void Start()
        {
            SetRandomRot();
        }


        public void InitCargo(MovementTypeSwitcher movementMovement)
        {
            _onTouchMovementMove = movementMovement;
        }
        
        void CargoMove()
        {
            transform.Translate(-_onTouchMovementMove.GetRocketDirection() * _onTouchMovementMove.CurrentSpeed/_smoothness * Time.deltaTime);
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
