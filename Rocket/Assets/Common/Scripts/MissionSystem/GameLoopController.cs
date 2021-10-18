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
    public class GameLoopController : MonoBehaviour
    {
        private SpaceObjectSpawnController _spaceObjectSpawnController;
        private GameStateMachine _gameStateMachine;


        [Inject]
        private void Constructor(RocketController rocketController,
            ObjectPoolStorage objectPoolStorage,
            IGameStateController gameStateController,
            ICoroutineRunner coroutineRunner, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            var objectController = new SpaceObjectLifeCycle(
                new SpaceObjectPoolWorker(objectPoolStorage),
                rocketController, gameStateController, this);

            _spaceObjectSpawnController = new SpaceObjectSpawnController(coroutineRunner, objectController,
                rocketController.Movement);
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
                    _gameStateMachine.Curtain.Show((() =>
                    {
                        _spaceObjectSpawnController.Disable();
                    }));
                    break;
            }
        }
    }
}