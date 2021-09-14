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
        private ISatelliteFactory _satelliteFactory;
        private RocketMovementController _rocketMovementController;

        private void OnEnable()
        {
            GameStateController.OnStateSwitch += GameStateListener;
        }

        private void OnDisable()
        {
            GameStateController.OnStateSwitch -= GameStateListener;
        }

        [Inject]
        private void Constructor(BasicSatelliteFactory satelliteFactory,RocketMovementController rocketMovementController)
        {
            _rocketMovementController = rocketMovementController;
            _satelliteFactory = satelliteFactory;
        }

        private IEnumerator WaitBeforeSatelliteSpawn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            CreateSpawner();
        }

        private void CreateSpawner()
        {
            ISpawner satelliteSpawner = new SatelliteSpawner(_satelliteFactory,_rocketMovementController.transform,_rocketMovementController.Rigidbody);
            satelliteSpawner.Spawn();
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(WaitBeforeSatelliteSpawn(_waitTime));
            }
        }

    }
}
