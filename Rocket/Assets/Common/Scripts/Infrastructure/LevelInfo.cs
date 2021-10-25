using System;
using Common.Scripts.MissionSystem;

namespace Common.Scripts.Infrastructure
{
    public class LevelInfo : ILevelInfo
    {
        private int _levelNumber;
        public event Action OnNextLevel;
        
        public void NextLevel()
        {
            _levelNumber++;
            OnNextLevel?.Invoke();
        }

        public int GetLevelNumber()
        {
            return _levelNumber;
        }
    }
}