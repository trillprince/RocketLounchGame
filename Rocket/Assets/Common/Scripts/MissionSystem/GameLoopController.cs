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
        private RocketMovementController _rocketMovementController;
        private LeftSatelliteController _leftSatelliteController;
        private RightSatelliteController _rightSatelliteController;
        private ObjectPoolStorage _objectPoolStorage;
        private RocketCargo _rocketCargo;
        private InputListener _inputListener;
        private SatelliteStateChanger _satelliteStateChanger;
        private SatelliteCount _satelliteCount;
        private SatelliteSystem _satelliteSystem;


        private void Awake()
        {
            _inputListener = GetComponent<InputListener>();
            _satelliteSystem = new SatelliteSystem();
            
            _leftSatelliteController = new LeftSatelliteController(
                new LeftSatelliteSpawner(_prefabOfSatellite, _rocketMovementController, _objectPoolStorage),
                _rocketMovementController);
            
            _rightSatelliteController = new RightSatelliteController(
                new RightSatelliteSpawner(_prefabOfSatellite, _rocketMovementController, _objectPoolStorage),
                _rocketMovementController);
            
            _satelliteStateChanger = new SatelliteStateChanger(_inputListener, 
                _leftSatelliteController,_rightSatelliteController, _rocketCargo,_satelliteCount);
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
            _leftSatelliteController.Execute();
            _rightSatelliteController.Execute();
            _satelliteStateChanger.Execute();
        }

        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage,
            RocketCargo rocketCargo,SatelliteCount satelliteCount)
        {
            _rocketMovementController = rocketMovementController;
            _objectPoolStorage = objectPoolStorage;
            _rocketCargo = rocketCargo;
            _satelliteCount = satelliteCount;
        }

        private IEnumerator WaitBeforeGameStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            var range = Random.Range(-2, 2);
            if (range < 0)
            {
                _leftSatelliteController.Spawn();
            }
            else
            {
                _rightSatelliteController.Spawn();
            }
            StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeSpawn));
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeStart));
            }
        }
    }

    public class SatelliteSystem
    {
        
    }
}