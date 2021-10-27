using System;
using Common.Scripts.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;
        

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show((() =>
            {
                _sceneLoader.Load(sceneName,OnLoaded);
            }));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
        
    }
}