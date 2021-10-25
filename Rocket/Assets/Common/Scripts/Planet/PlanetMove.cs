using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Planet
{
    public class PlanetMove : IMovableOnGameState 
    {
        private Vector3 _ladingPos;
        private int _moveSmoothness = 10;
        private Transform _planetTransform;
        private RocketSpeed _rocketSpeed;


        public PlanetMove(Transform planetTransform,RocketSpeed rocketSpeed)
        {
            _planetTransform = planetTransform;
            _ladingPos = planetTransform.position;
            _rocketSpeed = rocketSpeed;
        }

        void SetLandingPosition()
        {
            _planetTransform.position = _ladingPos;
        }

        public void Move()
        {
            _planetTransform.Translate(-_rocketSpeed.GetRocketDirection() * _rocketSpeed.CurrentSpeed/_moveSmoothness * Time.deltaTime);
        }

        public void OnGameStateSwitch(GameState gameState)
        {

        }
    }
}