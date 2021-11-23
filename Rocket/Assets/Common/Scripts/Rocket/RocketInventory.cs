using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class RocketInventory
    {
        private int _dogCoinValue = 0;

        public void AddCoinValue(int addValue)
        {
            _dogCoinValue += addValue;
        }

        public int GetCurrentCoinValue()
        {
            return _dogCoinValue;
        }
        
        
    }
}