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
        [SerializeField] private GameObject _asteroid;
        private SpaceObjectSystem _spaceObjectSystem;
        private GameStateController _gameStateController;
        

        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage,
            RocketCargo rocketCargo,GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            var inputListener = GetComponent<InputListener>();
            
            var leftSatelliteController = new LeftSpaceObjectController(
                new LeftSpaceObjectSpawner(_prefabOfSatellite, rocketMovementController, objectPoolStorage),
                rocketMovementController,gameStateController,this);
            
            var rightSatelliteController = new RightSpaceObjectController(
                new RightSpaceObjectSpawner(_prefabOfSatellite, rocketMovementController, objectPoolStorage),
                rocketMovementController,gameStateController,this);

            var middleSpaceObjectController = new MiddleSpaceObjectController(new MiddleSpaceObjectSpawner(_asteroid, rocketMovementController, objectPoolStorage),
                rocketMovementController,gameStateController,this);

            _spaceObjectSystem = new SpaceObjectSystem(inputListener,
                new SatelliteStateChanger(inputListener, 
                    leftSatelliteController,rightSatelliteController, rocketCargo),
                leftSatelliteController,rightSatelliteController,middleSpaceObjectController);
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
        
        private IEnumerator WaitBeforeGameStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (_gameStateController.CurrentGameState != GameState.CargoDrop)
            {
                yield break; 
            }
            _spaceObjectSystem.SpawnRandomSideSatellite();
            StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeSpawn));
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeStart));
                _spaceObjectSystem.Enable();
            }
        }

        public void DisableSatelliteDrop()
        {
            _gameStateController.SetStateToLanding((() =>
            {
                _spaceObjectSystem.Disable();
            }));
        }
    }
}