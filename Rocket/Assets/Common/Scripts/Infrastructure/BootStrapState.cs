using System;
using Common.Scripts.Firebase;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            EnterMenu();
        }

        private void EnterMenu()
        {
            
        }

        public void Exit()
        {
        }
    }
}