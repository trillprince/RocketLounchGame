using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Gameplay/Mission")]
    public class MissionInfo : ScriptableObject
    {
        [SerializeField] private string _nameOfMission;
        [SerializeField] private List <GameObject> _cargoList;
        [SerializeField] private List <int> cargoHightList;
        [SerializeField] private bool [] _cargosDelievered;
        [SerializeField] private List<Reward> _rewards;
        [SerializeField] private List<int> _rewardsAmount;

        
        public enum Reward
        {
            Coins,
            Fuel,
            Wrenches,
            Investors,
            Boxes
        }
        
        public List<int> CargoHightList => cargoHightList;

        public List<GameObject> CargoList => _cargoList;
    }
}