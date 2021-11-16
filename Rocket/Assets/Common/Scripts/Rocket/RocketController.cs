using System;
using System.Collections.Generic;
using Common.Scripts.Audio;
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
        public RocketInventory Inventory { get; private set; }
        public RocketDistance CoveredDistance { get; private set; }
        public RocketBoosterController BoosterController { get; private set; }
        public RocketAudio Audio { get; private set; }

        private Dictionary<Type, IGameStateSubscriber> _gameStateSubscribers;
        private ObjectPoolStorage _objectPoolStorage;
        private InputManager _inputManager;
        private IGameStateController _gameStateController;
        private PlayerDataSaver _playerDataSaver;
        public RocketCollisionController CollisionController { get; private set; }
        public RocketGraphics Graphics { get; private set; }

        [Inject]
        private void Constructor(IGameStateController gameStateController, ObjectPoolStorage objectPoolStorage,
            InputManager inputManager, ILevelInfo levelInfo, GameProgress gameProgress,LaunchManager launchManager,
            IAudioManager audioManager)
        {
            _playerDataSaver = gameProgress.PlayerDataSaver;
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

            Health = new RocketHealth(_gameStateController);

            Inventory = new RocketInventory();

            CoveredDistance = new RocketDistance(GetComponent<RocketSpeed>(), _playerDataSaver);

            BoosterController = new RocketBoosterController(this, Instantiate, Destroy);

            Audio = new RocketAudio(audioManager,launchManager);

            CollisionController = GetComponent<RocketCollisionController>();
            
            _gameStateSubscribers = new Dictionary<Type, IGameStateSubscriber>
            {
                [typeof(RocketMovement)] = Movement,
                [typeof(RocketDistance)] = CoveredDistance
            };

            Graphics = GetComponent<RocketGraphics>();
        }


        private void Update()
        {
            CoveredDistance.Execute();
            BoosterController.Execute();
        }

        private void OnEnable()
        {
            _gameStateController.OnStateSwitch += NotifyComponentsOnGameState;
            Audio.Enable();
        }

        private void OnDisable()
        {
            _gameStateController.OnStateSwitch -= NotifyComponentsOnGameState;
            Audio.Disable();
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