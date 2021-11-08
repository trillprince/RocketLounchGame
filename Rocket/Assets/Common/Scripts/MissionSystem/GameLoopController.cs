using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Audio;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour, IGameLoopController
    {
        private SpaceObjectSpawnController _spaceObjectSpawnController;
        private GameStateMachine _gameStateMachine;
        private GameProgress _gameProgress;
        private ILevelInfo _levelInfo;
        private RocketHealth _rocketHealth;
        private IGameTimeController _gameTimeController;
        private IGameStateController _gameStateController;
        public static event Action OnGameOver;


        [Inject]
        private void Constructor(RocketController rocketController,
            ObjectPoolStorage objectPoolStorage,
            IGameStateController gameStateController,
            ICoroutineRunner coroutineRunner, 
            GameStateMachine gameStateMachine,
            ILevelInfo levelInfo,
            IAudioManager audioManager,
            IGameTimeController gameTimeController)
        {
            _levelInfo = levelInfo;
            _rocketHealth = rocketController.Health; 
            _gameStateMachine = gameStateMachine;
            _gameStateController = gameStateController;
            var spaceObjectLifeCycle = new SpaceObjectLifeCycle(
                new SpaceObjectPoolWorker(objectPoolStorage),
                rocketController, gameStateController, this,audioManager);

            _spaceObjectSpawnController = new SpaceObjectSpawnController(coroutineRunner, spaceObjectLifeCycle,
                rocketController.Movement, _levelInfo);
            _gameTimeController = gameTimeController;
        }

        private void OnEnable()
        {
            _gameStateController.OnStateSwitch += GameStateListener;
        }

        private void OnDisable()
        {
            DisableGameLoop();
            _gameStateController.OnStateSwitch -= GameStateListener;
        }

        private void Update()
        {
            _spaceObjectSpawnController.Execute();
        }

        private void GameStateListener(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.CargoDrop:
                    _spaceObjectSpawnController.Enable();
                    break;
                case GameState.EndOfGame:
                    _gameStateMachine.Curtain.Show((() =>
                    {
                        DisableGameLoop();  
                        _gameTimeController.Pause();
                        OnGameOver?.Invoke();
                    }));
                    break;
            }
        }

        public void EnableGameLoop()
        {
            _spaceObjectSpawnController.Enable();
        }

        public void DisableGameLoop()
        {
            _spaceObjectSpawnController.Disable();
        }

    }
}