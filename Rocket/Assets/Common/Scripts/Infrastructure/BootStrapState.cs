namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial,onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<MenuBootStrapState,string>(SceneInfo.GetSceneName(SceneInfo.SceneName.Menu));


        public void  Exit()
        {
        
        }
    }
}
