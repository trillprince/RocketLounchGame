using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrapState: IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private MenuBootStrap _menuBootStrap;
        

        public MenuBootStrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload,MenuInit);
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