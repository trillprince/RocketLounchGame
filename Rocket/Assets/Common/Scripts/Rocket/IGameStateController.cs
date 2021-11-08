using System;

namespace Common.Scripts.Rocket
{
    public interface IGameStateController
    {
        public event Action <GameState>  OnStateSwitch;
        public void SetGameState(GameState state, Action action = null);
    }
}