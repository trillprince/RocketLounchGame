using System;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class GameLoopState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public GameLoopState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload,(() =>
            {
                _loadingCurtain.Hide();
            }));
        }

        public void Exit()
        {
        }

    }
}