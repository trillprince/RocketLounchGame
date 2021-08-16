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
        private RocketMovementController _rocketMovement;
        private int _moveSmoothness = 10;


        [Inject]
        void Contructor(RocketMovementController onTouchRocketMove)
        {
            _rocketMovement = onTouchRocketMove;
        }

        private void OnEnable()
        {
            LounchManager.OnRocketLounch += MovePlanet;
            DropStatusController.OnOutOfCargo += MoveToDefaultPos;
        }

        private void OnDisable()
        {
            LounchManager.OnRocketLounch -= MovePlanet;
            DropStatusController.OnOutOfCargo -= MoveToDefaultPos;
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

        public void MoveToDefaultPos()
        {
            transform.position = _startPos;
        }

    }
}