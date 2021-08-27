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
        

        public PlanetMove(Transform planetTransform)
        {
            _planetTransform = planetTransform;
            _ladingPos = planetTransform.position;
        }

        void SetLandingPosition()
        {
            _planetTransform.position = _ladingPos;
        }

        public void Move()
        {
            _planetTransform.Translate(-RocketSpeed.GetRocketDirection() * RocketSpeed.CurrentSpeed/_moveSmoothness * Time.deltaTime);
        }

        public void OnGameStateSwitch(GameState gameState)
        {
            if (gameState == GameState.Landing)
            {
                SetLandingPosition();
            }
        }
    }
}