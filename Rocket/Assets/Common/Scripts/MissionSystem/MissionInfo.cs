using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Gameplay/Mission")]
    public class MissionInfo : ScriptableObject
    {
        [SerializeField] private string _nameOfMission;
        [SerializeField] private List <GameObject> _cargoList;
        [SerializeField] private List <int> _cargoHignessList;
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


        public List<int> CargoHignessList
        {
            get => _cargoHignessList;
        }
    }
}