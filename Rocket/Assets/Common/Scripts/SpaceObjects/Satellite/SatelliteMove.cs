using Common.Scripts.Rocket;
using UnityEngine;

namespace Common.Scripts.Satellite
{
    public class SatelliteMove
    {
        private readonly RocketMovementController _rocketMovementController;
        private Transform _transform;
        private float _moveSmoothness = 12f;

        public SatelliteMove(RocketMovementController rocketMovementController, Transform transform)
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