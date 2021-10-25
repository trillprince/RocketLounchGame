using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using Common.Scripts.SpaceObjects;
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
        public static event Action OnGameOver;


        [Inject]
        private void Constructor(RocketController rocketController,
            ObjectPoolStorage objectPoolStorage,
            IGameStateController gameStateController,
            ICoroutineRunner coroutineRunner, 
            GameStateMachine gameStateMachine,
            ILevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
            _gameStateMachine = gameStateMachine;
            var spaceObjectLifeCycle = new SpaceObjectLifeCycle(
                new SpaceObjectPoolWorker(objectPoolStorage),
                rocketController, gameStateController, this);

            _spaceObjectSpawnController = new SpaceObjectSpawnController(coroutineRunner, spaceObjectLifeCycle,
                rocketController.Movement, _levelInfo);

        }

        private void OnEnable()
        {
            GameStateController.OnStateSwitch += GameStateListener;
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= GameStateListener;
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
                    DisableGameLoop();
                    OnGameOver?.Invoke();
                    break;
            }
        }

        public void EnableGameLoop()
        {
            _spaceObjectSpawnController.Enable();
        }

        public void DisableGameLoop()
        {
            _gameStateMachine.Curtain.Show(() => { _spaceObjectSpawnController.Disable(); });
        }

    }
}