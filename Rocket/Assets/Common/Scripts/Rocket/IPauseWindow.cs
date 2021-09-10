using System;

namespace Common.Scripts.Rocket
{
    public interface IPauseWindow
    {
        public void Constructor(Action onUnpauseAction);
        public void PauseTheGame();
        public void UnpauseTheGame();
    }
}