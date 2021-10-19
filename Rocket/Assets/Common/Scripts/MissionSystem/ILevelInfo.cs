using System;

namespace Common.Scripts.MissionSystem
{
    public interface ILevelInfo
    {
        public event Action OnNextLevel;
        public void NextLevel();
        public int GetLevelNumber();
    }
}