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
        [SerializeField] private GameObject _prefabCollectable;


        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage,
            RocketCargo rocketCargo, GameStateController gameStateController,ICoroutineRunner coroutineRunner)
        {
            _gameStateController = gameStateController;
            var inputListener = GetComponent<InputListener>();

            var objectController = new SpaceObjectController(
                new SpaceObjectFactory(rocketMovementController, objectPoolStorage,_prefabOfSatellite),
                rocketMovementController, gameStateController, this,new Queue<ISpaceObject>(20));

            _spaceObjectSystem = new SpaceObjectSystem(coroutineRunner,gameStateController,inputListener,
                objectController,new SpawnPositionController(rocketMovementController,
                    new LeftSpawnPosition(rocketMovementController),
                    new RightSpawnPosition(rocketMovementController),
                    new MiddleSpawnPosition(rocketMovementController)));
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