using System;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketLifeCycle : MonoBehaviour
    {
        private RocketCollision _rocketCollision;

        [Inject]
        public void Constructor(GameStateController gameStateController)
        {
            _rocketCollision = new RocketCollision(gameStateController);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _rocketCollision.ApplyCollision(other);
        }
    }
}