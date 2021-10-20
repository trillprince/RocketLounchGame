using System;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI;

namespace Common.Scripts.Rocket
{
    public interface IPauseWindow
    {
        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController, IGameLoopController gameLoopController, ILevelInfo levelInfo);
        public void Pause();
        public void UnPause();
    }
}