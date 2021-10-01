using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachineInstaller : MonoInstaller
    {


        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            var gameStateController = FindObjectOfType<GameStateController>();
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            
            Container.Bind<GameStateController>().FromInstance(gameStateController);
            Container.Bind<ICoroutineRunner>().FromInstance(gameBootstrapper.StateMachine.Loader.Runner);
            Container.Bind<GameStateMachine>().FromInstance(gameBootstrapper.StateMachine);
            Container.Bind<LoadingCurtain>().FromInstance(gameBootstrapper.StateMachine.Curtain);

        }
    }
}
