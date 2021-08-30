using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Infrastructure
{
    public class MenuBootStrapState: IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private MenuBootStrapper _menuBootStrapper;

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
            _menuBootStrapper = new MenuBootStrapper();
        }

        public void Exit()
        {
        }
    }
}