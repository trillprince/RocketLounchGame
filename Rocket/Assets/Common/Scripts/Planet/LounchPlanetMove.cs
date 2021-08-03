using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Planet
{
    public class LounchPlanetMove : MonoBehaviour
    {
        private bool _isMoving;
        private OnTouchRocketMove _onTouchRocketMove;
        [SerializeField] 
        [Range(10,40)]private int _moveSmoothness = 12;

        [Inject]
        void Contructor(OnTouchRocketMove onTouchRocketMove)
        {
            _onTouchRocketMove = onTouchRocketMove;
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
            transform.Translate(-_onTouchRocketMove.GetRocketDirection() * _onTouchRocketMove.RocketSpeed/_moveSmoothness * Time.deltaTime);
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