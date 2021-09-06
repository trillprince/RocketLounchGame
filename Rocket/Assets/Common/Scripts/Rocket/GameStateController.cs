using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Rocket
{
    public class GameStateController : MonoBehaviour
    {
        private float _timeBeforeStateSwitch = 3f;
        private GameState _currentGameState;
        public delegate void StateSwitch(GameState state);

        public static event StateSwitch OnStateSwitch;

        
        private void Start()
        {
            Debug.Log("Game state controller");
            OnStateSwitch?.Invoke(GameState.WaitForLaunch);
        }

        void OnEnable()
        {
            LaunchManager.OnRocketLounch += SetStateOnRocketLaunch;
            DropStatusController.OnOutOfCargo += SetStateOnOutOfCargo;
        }

        private void OnDisable()
        {
            LaunchManager.OnRocketLounch -= SetStateOnRocketLaunch;
            DropStatusController.OnOutOfCargo -= SetStateOnOutOfCargo;
        }

        void SetStateOnRocketLaunch()
        {
            SetGameState(GameState.CargoDrop);
        }

        void SetStateOnOutOfCargo()
        {
            StartCoroutine(WaitTillStateSwitch(GameState.Landing));
        }
        
        
        void SetGameState(GameState state)
        {
            _currentGameState = state;
            OnStateSwitch?.Invoke(state);
        }

        IEnumerator WaitTillStateSwitch(GameState gameState)
        {
            yield return new WaitForSeconds(_timeBeforeStateSwitch);
            SetGameState(gameState);
        }
    }


    public enum GameState
    {
        WaitForLaunch,
        CargoDrop,
        Landing,
        EndOfGame
    }
}