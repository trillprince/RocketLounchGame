using System;

namespace Common.Scripts.Rocket
{
    public interface IGameStateController
    {
        public void SetGameState(GameState state, Action action = null);
    }
}