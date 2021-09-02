using System;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        public GameStateMachine StateMachine { get; private set; }
        public SceneLoader Loader { get; private set; }

        [Inject]
        public void Constructor(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            StateMachine = gameStateMachine;
            Loader = sceneLoader;
        }

        private void Awake()
        {
            StateMachine.Enter<BootStrapState>();
            DontDestroyOnLoad(this);
        }

    }
}