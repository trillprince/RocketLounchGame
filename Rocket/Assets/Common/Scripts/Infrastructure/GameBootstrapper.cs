using System;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        public GameStateMachine StateMachine { get; private set; }

        [Inject]
        public void Constructor(GameStateMachine gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            StateMachine.Enter<BootStrapState>();
        }

    }
}