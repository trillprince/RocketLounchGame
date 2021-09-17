using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Input;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour
    {
        private float _waitTimeBeforeStart = 4;
        private float _waitTimeBeforeSpawn = 2;
        [SerializeField] private GameObject _prefabOfSatellite;
        private RocketMovementController _rocketMovementController;
        private SatelliteController _satelliteController;
        private ObjectPoolStorage _objectPoolStorage;
        private RocketCargo _rocketCargo;
        private bool _touchHold;

        private void Awake()
        {
           CreateSatelliteController();
        }

        private void OnEnable()
        {
            GameStateController.OnStateSwitch += GameStateListener;
            InputManager.OnTouchStart += vector2 =>
            {
                _touchHold = true;
            };
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= GameStateListener;
            InputManager.OnTouchEnd += () =>
            {
                _touchHold = false;
            };
        }

        private void Update()
        {
            _satelliteController.Execute();
            if (_touchHold && _satelliteController.SatellitesExist())
            {
                _touchHold = false;
                _rocketCargo.DropCargo(_satelliteController.ScopedSatellite);
                _satelliteController.DisposeScopedSatellite();
            }
        }

        [Inject]
        private void Constructor(RocketMovementController rocketMovementController, ObjectPoolStorage objectPoolStorage, RocketCargo rocketCargo)
        {
            _rocketMovementController = rocketMovementController;
            _objectPoolStorage = objectPoolStorage;
            _rocketCargo = rocketCargo;
        }

        private IEnumerator WaitBeforeGameStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _satelliteController.Spawn();
            StartCoroutine(WaitBeforeGameStart(_waitTimeBeforeSpawn));
        }

        private void UpdateScopedSatellite(ISatellite satellite)
        {
            _rocketCargo.UpdateSatellite(satellite);
        }

        private void CreateSatelliteController()
        {
            _satelliteController = new SatelliteController(
                new LeftSatelliteSpawner(_prefabOfSatellite,_rocketMovementController,_objectPoolStorage),
                new RightSatelliteSpawner(_prefabOfSatellite,_rocketMovementController,_objectPoolStorage),_rocketMovementController,UpdateScopedSatellite);
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
