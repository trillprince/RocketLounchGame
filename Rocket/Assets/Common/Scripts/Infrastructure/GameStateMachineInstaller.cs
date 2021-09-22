using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using Common.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private GameObject _coroutineRunner;

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateController>().FromInstance(FindObjectOfType<GameStateController>());
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            GameStateMachine stateMachine;
            if (gameBootstrapper == null)
            {
                stateMachine = new GameStateMachine(new SceneLoader(_coroutineRunner.GetComponent<ICoroutineRunner>()),_loadingCurtain);
                Container.Bind<GameStateMachine>().FromInstance(stateMachine);
                Container.Bind<LoadingCurtain>().FromInstance(stateMachine.Curtain);
                return;
            }
            Container.Bind<GameStateMachine>().FromInstance(gameBootstrapper.StateMachine);
            Container.Bind<LoadingCurtain>().FromInstance(gameBootstrapper.StateMachine.Curtain);
        }
    }
}
