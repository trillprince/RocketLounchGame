namespace Common.Scripts.Infrastructure
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootStrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            EnterMenu();
        }

        private void EnterMenu() => 
            _stateMachine.Enter<MenuBootStrapState>();

        public void  Exit()
        {
        
        }
    }
}
