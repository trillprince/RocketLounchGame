using Zenject;

namespace Common.Scripts.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().FromNew().AsSingle().WithArguments(new SceneLoader(this));
        }
    }
}


