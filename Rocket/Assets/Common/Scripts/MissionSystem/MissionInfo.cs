using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Gameplay/Mission")]
    public class MissionInfo : ScriptableObject
    {
        [SerializeField] private string _nameOfMission;
        [SerializeField] private MissionTier _tiefOfMission;
        [SerializeField] private List <GameObject> _cargoList;
        [SerializeField] private List <int> cargoHeightList;
        [SerializeField] private List<Reward> _rewards;
        [SerializeField] private List<int> _rewardsAmount;

        enum Reward
        {
            Coins,
            Fuel,
            Wrenches,
            Investors,
            Boxes
        }

        enum MissionTier
        {
            One,
            Two,
            Three,
            Four,
            Five
        }
        
        public List<int> CargoHeightList => cargoHeightList;

        public List<GameObject> CargoList => _cargoList;
        
    }
}