using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class AsteroidMove: IMoveComponent
    {
        private readonly RocketMovementController _rocketMovementController;
        private Transform _transform;
        private float _moveSmoothness = 12f;

        public AsteroidMove(RocketMovementController rocketMovementController, Transform transform)
        {
            _rocketMovementController = rocketMovementController;
            _transform = transform;
        }

        public void Move()
        {
            _transform.Translate((-_rocketMovementController.GetRocketDirection()*_rocketMovementController.GetRocketSpeed())/_moveSmoothness * Time.deltaTime);
        }
    }
}