using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Planet
{
    public class StartPlanet : MonoBehaviour
    {
        private bool _isMoving;
        private RocketMovement _rocketMovement;

        [Inject]
        void Contructor(RocketMovement rocketMovement)
        {
            _rocketMovement = rocketMovement;
        }

        private void OnEnable()
        {
            LounchManager.MiddleEngineEnable += MovePlanet;
        }

        private void OnDisable()
        {
            LounchManager.MiddleEngineEnable -= MovePlanet;
        }

        void MovePlanet(bool isMoving)
        {
            _isMoving = isMoving;
        }

        void PlanetMovement()
        {
            transform.Translate(-_rocketMovement.GetRocketDirection() * _rocketMovement.RocketSpeed/12 * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                PlanetMovement();
            }
        }
    }
}