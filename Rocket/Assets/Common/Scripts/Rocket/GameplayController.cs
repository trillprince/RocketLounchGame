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
    public class GameplayController : MonoBehaviour
    {
        private LoadingCurtain _loadingCurtain;
        private float _timeBeforeStateSwitch = 3f;

        public delegate void StateSwitch(GameState state);

        public static event StateSwitch OnStateSwitch;

        private event Action StateSwitching; 
    
        private void Awake()
        {
            _loadingCurtain = FindObjectOfType<LoadingCurtain>();
        }

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
                StartCoroutine(WaitTillStateSwitch());
            };
            StateSwitching += () =>
            {
                SetGameState(GameState.Landing);
            };
        }
        

        void SetGameState(GameState state)
        {
            OnStateSwitch?.Invoke(state);
            _loadingCurtain.Hide();
        }

        IEnumerator WaitTillStateSwitch()
        {
            yield return new WaitForSeconds(_timeBeforeStateSwitch); 
            _loadingCurtain.Show(StateSwitching);
        }
    }


    public enum GameState
    {
        WaitForLaunch,
        CargoDrop,
        Landing
    }
}