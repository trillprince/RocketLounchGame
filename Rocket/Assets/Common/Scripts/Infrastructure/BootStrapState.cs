namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootStrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            EnterMenu();
        }

        private void EnterMenu() => 
            _stateMachine.Enter<MenuBootStrapState,string>(SceneInfo.SceneName.Menu.ToString());

        public void  Exit()
        {
        
        }
    }
}
