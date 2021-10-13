using System;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketController : MonoBehaviour
    {
        public RocketCargo Cargo { get; private set; }
        public RocketMovement Movement { get; private set; }
        public RocketHealth Health { get; set; }

        private Dictionary<Type,IGameStateSubscriber> _gameStateSubscribers;

        [Inject]
        private void Constructor(ObjectPoolStorage objectPoolStorage,InputManager inputManager)
        {
            Cargo = new RocketCargo(objectPoolStorage,new AssetProvider(),transform);
            Movement = new RocketMovement(
                inputManager: inputManager,
                GetComponent<Rigidbody>(),
                transform: transform,
                GetComponentInChildren<MeshCollider>());

            Health = new RocketHealth();

            _gameStateSubscribers = new Dictionary<Type, IGameStateSubscriber>
            {
                [typeof(RocketMovement)] = Movement

            };
        }
        private void OnEnable()
        {
            GameStateController.OnStateSwitch += NotifyComponentsOnGameState;
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= NotifyComponentsOnGameState;
        }

        private void NotifyComponentsOnGameState(GameState gameState)
        {
            foreach (KeyValuePair<Type,IGameStateSubscriber> kvp in _gameStateSubscribers)
            {
                kvp.Value.OnGameStateChange(gameState);
            }
        }
    }
}


