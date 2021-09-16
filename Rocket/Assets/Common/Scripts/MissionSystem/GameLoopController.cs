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
        private float _waitTime = 4;
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
            _satelliteController.Enable();
        }

        private ISatelliteController CreateSatelliteController()
        {
            return  new SatelliteController(
                new SatelliteSpawner(_prefabOfSatellite,_rocketMovementController.transform,_rocketMovementController.Rigidbody,new ObjectPoolStorage()),_rocketMovementController);
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeGameStart(_waitTime));
            }
        }

    }
}
