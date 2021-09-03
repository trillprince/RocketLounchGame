using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrapState: IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private MenuBootStrap _menuBootStrap;
        private string _menuSceneName = "Menu";


        public MenuBootStrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(_menuSceneName,MenuInit);
        }

        private void MenuInit()
        {
            _menuBootStrap = new MenuBootStrap();
        }

        public void Exit()
        {
            
        }
    }
}