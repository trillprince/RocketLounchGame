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
        private MovementStateController _movementStateMovementState;
        private int _moveSmoothness = 10;


        [Inject]
        void Contructor(MovementStateController onTouchMovementStateMove)
        {
            _movementStateMovementState = onTouchMovementStateMove;
        }

        private void OnEnable()
        {
            LaunchManager.OnRocketLounch += MovePlanet;
            LandingController.Landing += OnChangeGameState;
            
        }

        private void OnDisable()
        {
            LaunchManager.OnRocketLounch -= MovePlanet;
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
            transform.Translate(-RocketSpeed.GetRocketDirection() * RocketSpeed.CurrentSpeed/_moveSmoothness * Time.deltaTime);
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