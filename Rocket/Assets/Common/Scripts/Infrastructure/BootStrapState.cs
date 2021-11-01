using System;
using Common.Scripts.Firebase;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private MenuBootStrap _menuBootstrap;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _menuBootstrap = new MenuBootStrap(new FirebaseBootStrap(),new Authentication());
            EnterMenu();
        }

        private void EnterMenu()
        {
            _stateMachine.Enter<MenuBootStrapState>();
        }

        public void Exit()
        {
        }
    }
}