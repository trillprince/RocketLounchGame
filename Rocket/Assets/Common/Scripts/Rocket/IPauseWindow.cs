using System;
using Common.Scripts.Infrastructure;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI;

namespace Common.Scripts.Rocket
{
    public interface IPauseWindow
    {
        public void Constructor(Action onUnpauseAction,IGameTimeController gameTimeController, 
            IGameLoopController gameLoopController,RocketController rocketController,PlayerDataSaver playerDataSaver,
            IGameStateController gameStateController);
        public void Pause();
        public void UnPause();
    }
}