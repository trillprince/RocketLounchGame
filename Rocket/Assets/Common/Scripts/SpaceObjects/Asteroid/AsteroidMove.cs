using Common.Scripts.Rocket;
using Common.Scripts.Satellite;
using UnityEngine;

namespace Common.Scripts.SpaceObjects.Satellite
{
    public class AsteroidMove: IMoveComponent
    {
        private readonly RocketMovementController _rocketMovementController;
        private Transform _transform;
        private float _moveSmoothness = 11f;

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