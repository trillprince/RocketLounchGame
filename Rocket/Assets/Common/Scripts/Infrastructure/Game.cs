namespace Common.Scripts.Infrastructure
{
    public class Game 
    {
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    
    }
}
