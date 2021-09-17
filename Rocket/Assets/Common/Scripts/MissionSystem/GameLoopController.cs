using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour
    {
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        private float _waitTimeBeforeStart = 4;
        private float _waitTimeBeforeSpawn = 2;
        [SerializeField] private GameObject _cargo;
        [SerializeField] private GameObject _prefabOfSatellite;
        private ISatelliteFactory _satelliteFactory;
        private RocketMovementController _rocketMovementController;
        private ISatelliteController _satelliteController;
        private bool _satelliteControllerEnabled;

        private void Awake()
        {
            _satelliteController = CreateSatelliteController();
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
            _satelliteController.Execute();
        }

        [Inject]
        private void Constructor(BasicSatelliteFactory satelliteFactory,RocketMovementController rocketMovementController)
        {
            _rocketMovementController = rocketMovementController;
            _satelliteFactory = satelliteFactory;
        }

        private IEnumerator WaitBeforeGameStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _satelliteController.Spawn();
            StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeSpawn));
        }

        private ISatelliteController CreateSatelliteController()
        {
            ObjectPoolStorage objectPoolStorage = new ObjectPoolStorage();
            return new SatelliteController(
                new LeftSatelliteSpawner(_prefabOfSatellite,_rocketMovementController.transform,_rocketMovementController.Rigidbody,objectPoolStorage),
                new RightSatelliteSpawner(_prefabOfSatellite,_rocketMovementController.transform,_rocketMovementController.Rigidbody,objectPoolStorage),_rocketMovementController);
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeStart));
            }
        }

    }
}
