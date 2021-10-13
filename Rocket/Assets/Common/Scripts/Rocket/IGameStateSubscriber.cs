namespace Common.Scripts.Rocket
{
    public interface IGameStateSubscriber
    {
        void OnGameStateChange(GameState gameState);
    }
}