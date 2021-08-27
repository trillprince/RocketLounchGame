using Common.Scripts.Rocket;

namespace Common.Scripts.Planet
{
    public interface IGameStateDependable
    {
        public void OnGameStateSwitch(GameState gameState);
    }
}