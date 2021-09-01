using System;

namespace Common.Scripts.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public GameLoopState(GameStateMachine stateMachine,SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        {
           
        }

        public void Enter()
        {
            
        }
    }
}