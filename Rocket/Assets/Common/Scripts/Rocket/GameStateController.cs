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
            OnStateSwitch?.Invoke(GameState.WaitForLaunch);
        }

        void OnEnable()
        {
            LaunchManager.OnRocketLounch += () =>
            {
                SetGameState(GameState.CargoDrop);
            };
            DropStatusController.OnOutOfCargo += () =>
            {
                StartCoroutine(WaitTillStateSwitch(GameState.Landing));
            };
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