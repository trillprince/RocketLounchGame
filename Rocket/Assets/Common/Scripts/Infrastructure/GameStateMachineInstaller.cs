using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        private GameBootstrapper _gameBootstrapper;


        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            var gameStateController = FindObjectOfType<GameStateController>();
            _gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            
            Container.Bind<GameStateController>().FromInstance(gameStateController);
            Container.Bind<ICoroutineRunner>().FromInstance(_gameBootstrapper.StateMachine.Loader.Runner);
            Container.Bind<GameStateMachine>().FromInstance(_gameBootstrapper.StateMachine);
            Container.Bind<LoadingCurtain>().FromInstance(_gameBootstrapper.StateMachine.Curtain);

        }

    }
}
