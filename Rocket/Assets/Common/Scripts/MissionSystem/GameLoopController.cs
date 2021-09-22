using System;
using System.Collections;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour
    {
        private float _waitTimeBeforeStart = 4;
        private float _waitTimeBeforeSpawn = 1;
        [SerializeField] private GameObject _prefabOfSatellite;
        private SatelliteSystem _satelliteSystem;
        private GameStateController _gameStateController;
        

        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage,
            RocketCargo rocketCargo,GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            var inputListener = GetComponent<InputListener>();
            
            var leftSatelliteController = new LeftSatelliteController(
                new LeftSatelliteSpawner(_prefabOfSatellite, rocketMovementController, objectPoolStorage),
                rocketMovementController,gameStateController,this);
            
            var rightSatelliteController = new RightSatelliteController(
                new RightSatelliteSpawner(_prefabOfSatellite, rocketMovementController, objectPoolStorage),
                rocketMovementController,gameStateController,this);

            _satelliteSystem = new SatelliteSystem(leftSatelliteController, rightSatelliteController, inputListener,
                new SatelliteStateChanger(inputListener, 
                    leftSatelliteController,rightSatelliteController, rocketCargo));
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
            _satelliteSystem.Execute();
        }
        
        private IEnumerator WaitBeforeGameStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (_gameStateController.CurrentGameState != GameState.CargoDrop)
            {
                yield break; 
            }
            _satelliteSystem.SpawnRandomSideSatellite();
            StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeSpawn));
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeStart));
            }
        }

        public void DisableSatelliteDrop()
        {
            _gameStateController.SetStateToLanding((() =>
            {
                _satelliteSystem.OnSystemDisable();
            }));
        }
    }
}