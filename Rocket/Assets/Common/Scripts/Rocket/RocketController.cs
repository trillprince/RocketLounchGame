using System;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class RocketController : MonoBehaviour
    {
        public RocketCargo Cargo { get; private set; }
        public RocketMovement Movement { get; private set; }
        public RocketHealth Health { get; private set; }
        public RocketInventory Inventory { get; set; }
        public RocketDistance CoveredDistance { get; set; }

        private Dictionary<Type, IGameStateSubscriber> _gameStateSubscribers;
        private ObjectPoolStorage _objectPoolStorage;
        private InputManager _inputManager;
        private IGameStateController _gameStateController;

        [Inject]
        private void Constructor(IGameStateController gameStateController, ObjectPoolStorage objectPoolStorage,
            InputManager inputManager, ILevelInfo levelInfo)
        {
            _objectPoolStorage = objectPoolStorage;
            _inputManager = inputManager;
            _gameStateController = gameStateController;

            Cargo = new RocketCargo(_objectPoolStorage, new AssetProvider(), transform);
            Movement = new RocketMovement(
                inputManager: _inputManager,
                GetComponent<Rigidbody>(),
                transform: transform,
                GetComponentInChildren<MeshCollider>(),
                GetComponent<RocketSpeed>());

            Health = new RocketHealth(_gameStateController, levelInfo);

            Inventory = new RocketInventory();

            CoveredDistance = new RocketDistance(GetComponent<RocketSpeed>());

            _gameStateSubscribers = new Dictionary<Type, IGameStateSubscriber>
            {
                [typeof(RocketMovement)] = Movement,
                [typeof(RocketDistance)] = CoveredDistance
            };
        }

        private void Update()
        {
            CoveredDistance.Execute();
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
            foreach (KeyValuePair<Type, IGameStateSubscriber> kvp in _gameStateSubscribers)
            {
                kvp.Value.OnGameStateChange(gameState);
            }
        }
    }
}