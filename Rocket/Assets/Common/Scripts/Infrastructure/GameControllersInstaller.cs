using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameControllersInstaller : MonoInstaller
    {
        private GameBootstrapper _gameBootstrapper;


        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            IGameStateController gameStateController = FindObjectOfType<GameStateController>();
            _gameBootstrapper = FindObjectOfType<GameBootstrapper>();

            Container.Bind<IGameLoopController>().FromInstance(FindObjectOfType<GameLoopController>());
            Container.Bind<ILevelInfo>().FromInstance(new LevelInfo());
            Container.Bind<IGameStateController>().FromInstance(gameStateController);
            Container.Bind<IGameTimeController>().FromInstance(new GameTimeController());
            Container.Bind<ICoroutineRunner>().FromInstance(_gameBootstrapper.StateMachine.Loader.Runner);
            Container.Bind<GameStateMachine>().FromInstance(_gameBootstrapper.StateMachine);
            Container.Bind<LoadingCurtain>().FromInstance(_gameBootstrapper.StateMachine.Curtain);

        }

    }
}
