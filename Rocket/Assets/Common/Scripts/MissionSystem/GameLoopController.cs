using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabOfSatellite;
        private SpaceObjectSystem _spaceObjectSystem;
        private GameStateController _gameStateController;


        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage,
            RocketCargo rocketCargo, GameStateController gameStateController,ICoroutineRunner coroutineRunner)
        {
            _gameStateController = gameStateController;
            var inputListener = GetComponent<InputListener>();
            SpaceObjectSpawner spaceObjectSpawner =
                new SpaceObjectSpawner(_prefabOfSatellite, rocketMovementController, objectPoolStorage);

            var leftSatelliteController = new LeftSpaceObjectController(
                spaceObjectSpawner,
                rocketMovementController, gameStateController, this,new Queue<ISpaceObject>(10));

            var rightSatelliteController = new RightSpaceObjectController(
                spaceObjectSpawner,
                rocketMovementController, gameStateController, this,new Queue<ISpaceObject>(10));

            var middleSpaceObjectController = new MiddleSpaceObjectController(
                spaceObjectSpawner,
                rocketMovementController, gameStateController, this,new Queue<ISpaceObject>(10));

            _spaceObjectSystem = new SpaceObjectSystem(rocketMovementController,coroutineRunner,gameStateController,inputListener,
                new SatelliteStateChanger(inputListener,
                    leftSatelliteController, rightSatelliteController, middleSpaceObjectController, rocketCargo),
                leftSatelliteController, rightSatelliteController, middleSpaceObjectController);
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
            _spaceObjectSystem.Execute();
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                _spaceObjectSystem.Enable();
            }
        }

        public void DisableSatelliteDrop()
        {
            _gameStateController.SetStateToLanding((() => { _spaceObjectSystem.Disable(); }));
        }
    }
}