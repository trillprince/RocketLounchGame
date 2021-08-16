using System;
using System.Collections.Generic;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Planet
{
    public class PlanetMove : MonoBehaviour
    {
        private bool _isMoving;
        private Vector3 _startPos;
        private RocketControl _rocketMovement;
        private int _moveSmoothness = 10;


        [Inject]
        void Contructor(RocketControl onTouchRocketMove)
        {
            _rocketMovement = onTouchRocketMove;
        }

        private void OnEnable()
        {
            LounchManager.OnRocketLounch += MovePlanet;
            LandingController.Landing += OnChangeGameState;
            
        }

        private void OnDisable()
        {
            LounchManager.OnRocketLounch -= MovePlanet;
            LandingController.Landing -= OnChangeGameState;
        }

        private void OnChangeGameState()
        {
            MoveToDefaultPos();
            MovePlanet(false);
        }

        private void Start()
        {
            _startPos = transform.position;
        }
        
        void MovePlanet(bool isMoving)
        {
            _isMoving = isMoving;
        }

        void PlanetMovement()
        {
            transform.Translate(-_rocketMovement.GetRocketDirection() * _rocketMovement.RocketSpeed/_moveSmoothness * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                PlanetMovement();
            }
        }
        void MoveToDefaultPos()
        {
            transform.position = _startPos;
        }

    }
}