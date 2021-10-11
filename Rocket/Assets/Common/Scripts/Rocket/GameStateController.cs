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
        public GameState CurrentGameState { get; private set; }

        public delegate void StateSwitch(GameState state);

        public static event StateSwitch OnStateSwitch;
        private LoadingCurtain _loadingCurtain;

        private void Awake()
        {
            _loadingCurtain = FindObjectOfType<BootstrapAgregator>().Curtain;
        }


        private void Start()
        {
            OnStateSwitch?.Invoke(GameState.WaitForLaunch);
        }

        void OnEnable()
        {
            LaunchManager.OnRocketLaunch += SetStateOnRocketLaunch;
        }

        private void OnDisable()
        {
            LaunchManager.OnRocketLaunch -= SetStateOnRocketLaunch;
        }

        void SetStateOnRocketLaunch()
        {
            SetGameState(GameState.CargoDrop);
        }

        
        public void SetGameState(GameState state,Action action = null)
        {
            CurrentGameState = state;
            OnStateSwitch?.Invoke(state);
            action?.Invoke();
        }

        IEnumerator WaitTillStateSwitch(GameState gameState,Action action = null)
        {
            yield return new WaitForSeconds(_timeBeforeStateSwitch);
            SetGameState(gameState,action);
        }
    }


    public enum GameState
    {
        WaitForLaunch,
        CargoDrop,
        EndOfGame
    }
}