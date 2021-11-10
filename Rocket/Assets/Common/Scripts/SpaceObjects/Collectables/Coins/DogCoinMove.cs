using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.SpaceObjects.Collectables.Coins
{
    public class DogCoinMove : CollectableMove
    {
        private readonly Transform _transform;

        public DogCoinMove(RocketMovement rocketControllerMovement, Transform transform): 
            base(rocketControllerMovement,transform)
        {
            _transform = transform;
        }

        public override void Move()
        {
            base.Move();
            Rotate();
        }

        private void Rotate()
        {
            _transform.Rotate(0, 40 * Time.deltaTime,0);
        }
    }
}