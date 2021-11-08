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


        public event Action <GameState>  OnStateSwitch;
        private LoadingCurtain _loadingCurtain;
        private LaunchManager _launchManager;
        private RocketController _rocketController;

        [Inject]
        public void Constructor(LaunchManager launchManager, RocketController rocketController)
        {
            _launchManager = launchManager;
            _rocketController = rocketController;
        }

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
            _launchManager.OnRocketLaunch += SetStateOnRocketLaunch;
            _rocketController.Health.OnRocketDestroy += (() => { SetGameState(GameState.EndOfGame);});
        }

        private void OnDisable()
        {
            _launchManager.OnRocketLaunch -= SetStateOnRocketLaunch;
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