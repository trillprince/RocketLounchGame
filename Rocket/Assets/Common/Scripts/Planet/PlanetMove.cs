using System;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Planet
{
    public class PlanetMove : MonoBehaviour
    {
        private bool _isMoving;
        private OnTouchRocketMove _onTouchRocketMove;
        private int _moveSmoothness = 10;


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