using System;
using System.Runtime.Serialization;
using TMPro;

namespace Common.Scripts.Data
{
   [Serializable]
    public class PlayerData: ISerializable
    {
        private string _name;
        private static string _playerDataKey = "PlayerDataKey";
        private int _coins;
        private int _wrenches;
        private int _investors;
        
        public PlayerData(string name , int coins, int wrenches, int investors)
        {
            _name = name;
            _coins = coins;
            _wrenches = wrenches;
            _investors = investors;
        }
        
        protected PlayerData(SerializationInfo info, StreamingContext context)
        {
            _name = info.GetString("_name");
            _coins = info.GetInt32("_coins");
            _wrenches = info.GetInt32("_wrenches");
            _investors = info.GetInt32("_investors");
        }
        
        public static string GetDataKey()
        {
            return _playerDataKey;
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_name", _name);
            info.AddValue("_coins", _coins);
            info.AddValue("_wrenches", _wrenches);
            info.AddValue("_investors", _investors);
        }
    }
    
}