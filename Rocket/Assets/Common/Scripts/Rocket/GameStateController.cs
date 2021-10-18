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
    public class GameStateController : MonoBehaviour,IGameStateController
    {
        private GameState CurrentGameState { get; set; }

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
            if (CurrentGameState != state)
            {
                CurrentGameState = state;
                OnStateSwitch?.Invoke(state);
                action?.Invoke();
            }
        }
    }


    public enum GameState
    {
        WaitForLaunch,
        CargoDrop,
        EndOfGame
    }
}