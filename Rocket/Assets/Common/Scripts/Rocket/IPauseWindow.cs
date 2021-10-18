using System;
using Common.Scripts.UI;

namespace Common.Scripts.Rocket
{
    public interface IPauseWindow
    {
        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController);
        public void Pause();
        public void UnPause();
    }
}