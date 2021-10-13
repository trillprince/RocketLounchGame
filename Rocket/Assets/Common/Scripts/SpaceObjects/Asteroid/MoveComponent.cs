using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects.Asteroid
{
    public class MoveComponent: IMoveComponent
    {
        private readonly RocketMovement _rocketMovement;
        private readonly Transform _transform;
        private float _moveSmoothness = 11f;

        protected MoveComponent(RocketMovement rocketMovement, Transform transform)
        {
            _rocketMovement = rocketMovement;
            _transform = transform;
        }

        public void Move()
        {
            _transform.Translate((-_rocketMovement.GetRocketDirection()*_rocketMovement.GetRocketSpeed())/_moveSmoothness * Time.deltaTime);

        }
    }
}