using System;
using Common.Scripts.UI;

namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private string _menuSceneName = "Menu";

        public BootStrapState(GameStateMachine stateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader)
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
            _sceneLoader.Load(_menuSceneName,(() =>
            {
                _stateMachine.Enter<MenuBootStrapState>();
            }));
        } 
            

        public void  Exit()
        {
        
        }
    }
}
