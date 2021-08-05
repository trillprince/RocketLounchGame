using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Gameplay/Mission")]
    public class CurrentMissionInfo : ScriptableObject
    {
        [SerializeField] private string _nameOfMission;
        [SerializeField] private List <GameObject> _cargoList;
        [SerializeField] private List <int> cargoHeightList;

        public List<int> CargoHeightList => cargoHeightList;

        public List<GameObject> CargoList => _cargoList;
        
    }

}